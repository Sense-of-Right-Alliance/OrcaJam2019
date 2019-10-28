using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject SpiderSwarmVisualPrefab;

    private readonly Player[] _players = new Player[4];

    public IList<Player> Players => _players.Where(p => p != null).ToList();

    private readonly List<string> _inputButtons = new List<string>
    {
        "A",
        "B",
        "X",
        "Y",
    };

    private ScarecrowManager _scarecrowManager;
    private UiManager _uiManager;

    private void Awake()
    {
        _uiManager = Utility.UiManager;
        _scarecrowManager = Utility.ScarecrowManager;
    }

    private void Start()
    {

    }

    private void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (_players[i] == null && InputButtonIsPressed(i + 1))
            {
                AddPlayer(i);
            }
        }
    }

    private bool InputButtonIsPressed(int id)
    {
        return _inputButtons.Any(button => Input.GetButtonDown(button + id));
    }

    public Player GetPlayer(int playerId)
    {
        return _players.SingleOrDefault(p => p.Id == playerId);
    }

    private void AddPlayer(int id)
    {
        if (id < 0 || id > 4 || _players[id] != null)
        {
            return;
        }

        var playerGameObject = Instantiate(playerPrefab);
        playerGameObject.name = $"Player {id + 1}";

        var player = playerGameObject.GetComponent<Player>();
        player.Id = id;
        _players[id] = player;

        _uiManager.ShowResource(id);
        _scarecrowManager.ReassignPlayersToScarecrows(_players);
    }

    public void PlaySpiderSwarms()
    {
        List<Scarecrow> scarecrows = _scarecrowManager.ScarecrowsLeftToRight.ToList();
        for (int i = 0; i < scarecrows.Count; i++)
        {
            if (scarecrows[i].IsIntact)
            {
                GameObject spiders = Instantiate(SpiderSwarmVisualPrefab);

                spiders.transform.position = new Vector3(scarecrows[i].transform.position.x, scarecrows[i].transform.position.y - 5, 10);
                spiders.GetComponent<SpiderSwarmVisual>().SetTarget(new Vector3(scarecrows[i].transform.position.x, scarecrows[i].transform.position.y, 10));
            }
        }
    }
}
