using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] int resources = 0;

    [SerializeField] GameObject selectorPrefab;
    [SerializeField] GameObject resourceGooberPrefab;

    public int Id
    {
        get => id;
        set => id = value;
    }

    public class ResourceChangedEvent : UnityEvent<int, int> { }
    public static ResourceChangedEvent OnResourceChanged { get; } = new ResourceChangedEvent();

    public int Resources
    {
        get => resources;
        set
        {
            resources = value;
            OnResourceChanged.Invoke(Id, resources);
        }
    }

    private UiManager _uiManager;
    private GameObject _selector;

    private void Awake()
    {
        _uiManager = Utility.UiManager;
    }

    private void Start()
    {
        _selector = Instantiate(selectorPrefab);
        _selector.GetComponent<PlayerSelector>().Init(this);
    }

    private void Update()
    {
        
    }

    public void RepairPart(Scarecrow scarecrow, ScarecrowPart part)
    {
        int repairAmount = 1;
        if (Resources >= repairAmount && part.State == ScarecrowPartState.Intact)
        {
            scarecrow.RepairPart(part.Type, repairAmount);
            Resources -= repairAmount;

            for (int i = 0; i < repairAmount; i++)
            {
                var goober = Instantiate(resourceGooberPrefab);
                goober.transform.position = _uiManager.GetPlayerResourceBoxPosition(Id);
                Vector2 target = _selector.transform.position;//part.transform.TransformPoint(part.transform.position);
                goober.GetComponent<ResourceGoober>().SetTarget(target);
            }
        }
    }
}
