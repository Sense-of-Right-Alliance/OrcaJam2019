using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] int resources = 0;
    [SerializeField] int id = 0;

    [SerializeField] GameObject selectorPrefab;
    [SerializeField] GameObject resourceGooberPrefab;

    public class ResourceChangedEvent : UnityEvent<int, int> { }
    public static ResourceChangedEvent OnResourceChanged { get; } = new ResourceChangedEvent();

    public int Resources
    {
        get => resources;
        set
        {
            resources = value;
            OnResourceChanged.Invoke(0, resources);
        }
    }

    private UiManager _uiManager;
    private GameObject _selector;

    private void Awake()
    {
        _selector = Instantiate(selectorPrefab);
        _selector.GetComponent<PlayerSelector>().Init(this);

        _uiManager = Utility.UiManager;
    }

    private void Start()
    {

    }

    private void Update()
    {
        
    }

    public void RepairPart(Scarecrow scarecrow, ScarecrowPart part)
    {
        int repairAmount = 1;
        if (Resources >= repairAmount)
        {
            scarecrow.RepairPart(part.Type, repairAmount);
            Resources -= repairAmount;

            for (int i = 0; i < repairAmount; i++)
            {
                var goober = Instantiate(resourceGooberPrefab);
                goober.transform.position = _uiManager.GetPlayerResourceBoxPosition(id);
                Vector2 target = _selector.transform.position;//part.transform.TransformPoint(part.transform.position);
                goober.GetComponent<ResourceGoober>().SetTarget(target);
            }
        }
    }
}
