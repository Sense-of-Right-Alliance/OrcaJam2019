using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeasonEventManager : MonoBehaviour
{
    [SerializeField] private int lightningStrikeCountMin = 0;
    [SerializeField] private int lightningStrikeCountMax = 2;
    [SerializeField] private int lightningStrikeDamageMin = 30;
    [SerializeField] private int lightningStrikeDamageMax = 40;
    [SerializeField] private float lightningStrikeFireChance = 0.5f;
    [SerializeField] private int blizzardDamageMin = 10;
    [SerializeField] private int blizzardDamageMax = 20;
    [SerializeField] private int tornadoDamageMin = 20;
    [SerializeField] private int tornadoDamageMax = 40;
    [SerializeField] private int meteoriteCountMin = 8;
    [SerializeField] private int meteoriteCountMax = 20;
    [SerializeField] private int meteoriteDamageMin = 2;
    [SerializeField] private int meteoriteDamageMax = 5;
    [SerializeField] private int asteroidDamageMin = 40;
    [SerializeField] private int asteroidDamageMax = 60;

    [SerializeField] GameObject GentleRainParticleEffectPrefab;
    GameObject gentleRainParticleEffect;

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

        switch (seasonEvent.Type)
        {
            case SeasonEventType.None:
                return;
            case SeasonEventType.GentleRain:
                ProcessGentleRain(seasonEvent.Duration);
                break;
            case SeasonEventType.Thunderstorm:
                ProcessThunderstorm();
                break;
            case SeasonEventType.Blizzard:
                ProcessBlizzard();
                break;
            case SeasonEventType.Tornado:
                ProcessTornado();
                break;
            case SeasonEventType.MeteoriteStorm:
                ProcessMeteoriteStorm();
                break;
            case SeasonEventType.AsteroidStrike:
                ProcessAsteroidStrike();
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
        gentleRainParticleEffect = (GameObject)Instantiate(GentleRainParticleEffectPrefab);
        gentleRainParticleEffect.GetComponent<GentleRainVisual>().Init(duration);

        foreach (var scarecrow in _scarecrowManager.ScarecrowsLeftToRight)
        {
            scarecrow.SetWet();
        }
    }

    private void ProcessThunderstorm()
    {
        var intactScarecrows = _scarecrowManager.ScarecrowsLeftToRight.Where(s => s.IsIntact).ToList();

        foreach (var scarecrow in intactScarecrows)
        {
            scarecrow.SetWet();
        }

        int lightningStrikes = RandomBetween(lightningStrikeCountMin, lightningStrikeCountMax);
        for (int i = 0; i < lightningStrikes; i++)
        {
            var scarecrow = RandomScarecrow(intactScarecrows);
            int damage = RandomBetween(lightningStrikeDamageMin, lightningStrikeDamageMax);
            scarecrow.DamageRandomPart(damage);

            if (Random.Range(0f, 1f) < lightningStrikeFireChance)
            {
                scarecrow.SetAflame();
            }
        }
    }

    private void ProcessBlizzard()
    {
        bool leftToRight = Random.Range(0f, 1f) < 0.5f;
        var scarecrows = leftToRight ? _scarecrowManager.ScarecrowsLeftToRight : _scarecrowManager.ScarecrowsRightToLeft;

        int damage = RandomBetween(blizzardDamageMin, blizzardDamageMax);
        foreach (var scarecrow in scarecrows)
        {
            scarecrow.DamageAllParts(damage);
        }
    }

    private void ProcessTornado()
    {
        TornadoDirection direction;
        IEnumerable<Scarecrow> scarecrows;

        float random = Random.Range(0f, 1f);
        if (random < 0.33f)
        {
            direction = TornadoDirection.LeftToRight;
            scarecrows = new List<Scarecrow> { _scarecrowManager.OuterRightScarecrow };
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

        foreach (var scarecrow in scarecrows)
        {
            scarecrow.DamageRandomPart(RandomBetween(tornadoDamageMin, tornadoDamageMax));
            scarecrow.DamageRandomPart(RandomBetween(tornadoDamageMin, tornadoDamageMax));
        }
    }

    private void ProcessMeteoriteStorm()
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

        for (int i = 0; i < meteorites; i++)
        {
            var scarecrow = RandomScarecrow(scarecrows);
            scarecrow.DamageRandomPart(RandomBetween(meteoriteDamageMin, meteoriteDamageMax));
        }
    }

    private void ProcessAsteroidStrike()
    {
        int fullDamage = RandomBetween(asteroidDamageMin, asteroidDamageMax);
        int halfDamage = fullDamage / 2;

        foreach (var scarecrow in _scarecrowManager.CentreScarecrows)
        {
            scarecrow.DamageAllParts(fullDamage);
        }

        foreach (var scarecrow in _scarecrowManager.CentreScarecrows)
        {
            scarecrow.DamageAllParts(halfDamage);
        }
    }
}
