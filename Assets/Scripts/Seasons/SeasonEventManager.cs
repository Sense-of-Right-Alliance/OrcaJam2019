using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeasonEventManager : MonoBehaviour
{
    [SerializeField] private int lightningStrikeCountMin = 2;
    [SerializeField] private int lightningStrikeCountMax = 5;
    [SerializeField] private int lightningStrikeDamageMin = 20;
    [SerializeField] private int lightningStrikeDamageMax = 40;
    [SerializeField] private float lightningStrikeFireChance = 1f;//0.5f;
    [SerializeField] private int blizzardDamageMin = 10;
    [SerializeField] private int blizzardDamageMax = 20;
    [SerializeField] private int tornadoDamageMin = 20;
    [SerializeField] private int tornadoDamageMax = 30;
    [SerializeField] private int meteoriteCountMin = 8;
    [SerializeField] private int meteoriteCountMax = 20;
    [SerializeField] private int meteoriteDamageMin = 5;
    [SerializeField] private int meteoriteDamageMax = 10;
    [SerializeField] private int asteroidDamageMin = 20;
    [SerializeField] private int asteroidDamageMax = 40;
    [SerializeField] private int fireDamage = 3;

    [SerializeField] private int gentleRainResources = 2;
    [SerializeField] private float lightningStrikeRefundRate = 0.1f;
    [SerializeField] private float blizzardRefundRate = 0f;
    [SerializeField] private float tornadoRefundRate = 0f;//0.1f;
    [SerializeField] private float meteoriteRefundRate = 0f;//0.2f;
    [SerializeField] private float asteroidRefundRate = 0f;//0.1f;

    [SerializeField] GameObject gentleRainVisualEffectPrefab;
    [SerializeField] GameObject meteoriteShowerVisualPrefab;
    [SerializeField] GameObject asteroidStrikeVisualPrefab;
    [SerializeField] GameObject tornadoVisualPrefab;
    [SerializeField] GameObject blizzardVisualPrefab;
    [SerializeField] GameObject lightningStormVisualPrefab; 


    private ScarecrowManager _scarecrowManager;

    private void Awake()
    {
        _scarecrowManager = Utility.ScarecrowManager;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void ProcessSeasonEvent(SeasonEvent seasonEvent)
    {
        if (seasonEvent == null)
        {
            return;
        }

        foreach (var scarecrow in _scarecrowManager.ScarecrowsLeftToRight)
        {
            if (scarecrow.IsAflame)
            {
                scarecrow.DamageAllParts(fireDamage);
            }
        }

        switch (seasonEvent.Type)
        {
            case SeasonEventType.None:
                return;
            case SeasonEventType.GentleRain:
                ProcessGentleRain(seasonEvent.Duration);
                break;
            case SeasonEventType.Thunderstorm:
                ProcessThunderstorm(seasonEvent.Duration);
                break;
            case SeasonEventType.Blizzard:
                ProcessBlizzard(seasonEvent.Duration);
                break;
            case SeasonEventType.Tornado:
                ProcessTornado(seasonEvent.Duration);
                break;
            case SeasonEventType.MeteoriteStorm:
                ProcessMeteoriteStorm(seasonEvent.Duration);
                break;
            case SeasonEventType.AsteroidStrike:
                ProcessAsteroidStrike(seasonEvent.Duration);
                break;
            default:
                throw new System.ArgumentOutOfRangeException();
        }
    }

    private int RandomBetween(int min, int max)
    {
        return Random.Range(min, max + 1);
    }

    private Scarecrow RandomScarecrow(IReadOnlyCollection<Scarecrow> scarecrows)
    {
        var index = Random.Range(0, scarecrows.Count);
        return scarecrows.ElementAt(index);
    }

    private void ProcessGentleRain(float duration)
    {
        var gentleRainVisual = Instantiate(gentleRainVisualEffectPrefab);
        gentleRainVisual.GetComponent<GentleRainVisual>().Init(duration);

        StartCoroutine(DelayedGentleRainEffects(duration));
    }

    private IEnumerator DelayedGentleRainEffects(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (var scarecrow in _scarecrowManager.ScarecrowsLeftToRight)
        {
            scarecrow.SetWet();
        }

        _scarecrowManager.AssignResourcesToAllPlayers(gentleRainResources);
    }

    private void ProcessThunderstorm(float duration)
    {
        var intactScarecrows = _scarecrowManager.ScarecrowsLeftToRight.Where(s => s.IsIntact).ToList();
        
        int lightningStrikes = RandomBetween(lightningStrikeCountMin, lightningStrikeCountMax);
        var targets = new ScarecrowPart[lightningStrikes];
        for (int i = 0; i < lightningStrikes; i++)
        {
            var scarecrow = RandomScarecrow(intactScarecrows);
            var part = scarecrow.GetRandomPart();
            targets[i] = part;
        }

        var lightningStormVisual = Instantiate(lightningStormVisualPrefab);
        lightningStormVisual.GetComponent<LightningStormVisual>().Init(duration, lightningStrikes, targets);

        StartCoroutine(DelayedThunderstormEffects(targets, duration));
    }

    private IEnumerator DelayedThunderstormEffects(IEnumerable<ScarecrowPart> parts, float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (var part in parts)
        {
            int damage = RandomBetween(lightningStrikeDamageMin, lightningStrikeDamageMax);
            part.Damage(damage);

            if (Random.Range(0f, 1f) < lightningStrikeFireChance)
            {
                part.Scarecrow.SetAflame();
            }
            part.Scarecrow.Player.Resources += (int)(damage * lightningStrikeRefundRate);
        }
    }

    private void ProcessBlizzard(float duration)
    {
        bool leftToRight = Random.Range(0f, 1f) < 0.5f;
        var scarecrows = leftToRight ? _scarecrowManager.ScarecrowsLeftToRight : _scarecrowManager.ScarecrowsRightToLeft;

        var targets = new List<ScarecrowPart>();
        foreach (var scarecrow in scarecrows)
        {
            targets.AddRange(scarecrow.Parts.Where(p => p.State == ScarecrowPartState.Intact));
        }
        
        var blizzardVisual = Instantiate(blizzardVisualPrefab);
        blizzardVisual.GetComponent<BlizzardVisual>().Init(duration, leftToRight, targets);

        StartCoroutine(DelayedBlizzardEffects(targets, duration));
    }

    private IEnumerator DelayedBlizzardEffects(IEnumerable<ScarecrowPart> parts, float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (var scarecrow in _scarecrowManager.ScarecrowsLeftToRight)
        {
            scarecrow.RemoveAflame();
        }

        var scarecrows = parts.Select(p => p.Scarecrow).Distinct();

        int damage = RandomBetween(blizzardDamageMin, blizzardDamageMax);
        foreach (var scarecrow in scarecrows)
        {
            scarecrow.DamageAllParts(damage);
        }
        _scarecrowManager.AssignResourcesToAllPlayers((int)(damage * blizzardRefundRate));
    }

    private void ProcessTornado(float duration)
    {
        TornadoDirection direction;
        IEnumerable<Scarecrow> scarecrows;

        float random = Random.Range(0f, 1f);
        if (random < 0.33f)
        {
            direction = TornadoDirection.LeftToRight;
            scarecrows = new List<Scarecrow> { _scarecrowManager.OuterLeftScarecrow };
        }
        else if (random < 0.66f)
        {
            direction = TornadoDirection.RightToLeft;
            scarecrows = new List<Scarecrow> { _scarecrowManager.OuterRightScarecrow };
        }
        else
        {
            direction = TornadoDirection.FrontToBack;
            scarecrows = _scarecrowManager.OuterScarecrows;
        }

        var targets = new List<ScarecrowPart>();
        foreach (var scarecrow in scarecrows)
        {
            targets.Add(scarecrow.GetRandomPart());
            targets.Add(scarecrow.GetRandomPart());
        }

        var tornadoVisual = Instantiate(tornadoVisualPrefab);
        tornadoVisual.GetComponent<TornadoVisual>().Init(duration, direction, targets);

        StartCoroutine(DelayedTornadoEffects(targets, duration));
    }

    private IEnumerator DelayedTornadoEffects(IEnumerable<ScarecrowPart> parts, float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (var part in parts)
        {
            var scarecrow = part.Scarecrow;
            var part1 = scarecrow.DamageRandomPart(RandomBetween(tornadoDamageMin, tornadoDamageMax));
            var part2 = scarecrow.DamageRandomPart(RandomBetween(tornadoDamageMin, tornadoDamageMax));

            scarecrow.Player.Resources += (int)(RandomBetween(tornadoDamageMin, tornadoDamageMax) * tornadoRefundRate);
        }
    }

    private void ProcessMeteoriteStorm(float duration)
    {
        int meteorites = RandomBetween(meteoriteCountMin, meteoriteCountMax);
        var scarecrows = new List<Scarecrow>
        {
            _scarecrowManager.OuterLeftScarecrow,
            _scarecrowManager.CentreLeftScarecrow,
            _scarecrowManager.CentreLeftScarecrow,
            _scarecrowManager.CentreRightScarecrow,
            _scarecrowManager.CentreRightScarecrow,
            _scarecrowManager.OuterRightScarecrow,
        };

        var targets = new ScarecrowPart[meteorites];
        for (int i = 0; i < meteorites; i++)
        {
            var scarecrow = RandomScarecrow(scarecrows);
            targets[i] = scarecrow.GetRandomPart();
        }

        var meteoriteVisual = Instantiate(meteoriteShowerVisualPrefab);
        meteoriteVisual.GetComponent<MeteoriteShowerVisual>().Init(duration, meteorites, targets);

        StartCoroutine(DelayedMeteorStormEffects(targets, duration));
    }

    private IEnumerator DelayedMeteorStormEffects(IEnumerable<ScarecrowPart> parts, float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (var part in parts)
        {
            var scarecrow = part.Scarecrow;
            var damage = RandomBetween(meteoriteDamageMin, meteoriteDamageMax);
            scarecrow.DamagePart(part.Type, damage);
            scarecrow.Player.Resources += (int)(damage * meteoriteRefundRate);
        }
    }

    private void ProcessAsteroidStrike(float duration)
    {
        var asteroidVisual = Instantiate(asteroidStrikeVisualPrefab);
        asteroidVisual.GetComponent<AsteroidStrikeVisual>().Init(duration);

        StartCoroutine(DelayedAsteroidStrikeEffects(duration));
    }

    private IEnumerator DelayedAsteroidStrikeEffects(float delay)
    {
        yield return new WaitForSeconds(delay);

        int fullDamage = RandomBetween(asteroidDamageMin, asteroidDamageMax);
        int halfDamage = fullDamage / 2;

        foreach (var scarecrow in _scarecrowManager.CentreScarecrows)
        {
            scarecrow.DamageAllParts(fullDamage);
            scarecrow.Player.Resources += (int)(fullDamage * asteroidRefundRate);
        }

        foreach (var scarecrow in _scarecrowManager.OuterScarecrows)
        {
            scarecrow.DamageAllParts(halfDamage);
            scarecrow.Player.Resources += (int)(halfDamage * asteroidRefundRate);
        }
    }
}
