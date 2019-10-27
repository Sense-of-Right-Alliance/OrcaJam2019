using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public class ResourceChangedEvent : UnityEvent<int, int> { }
    public static ResourceChangedEvent OnResourceChanged { get; } = new ResourceChangedEvent();

    [SerializeField] GameObject SelectorPrefab;
    [SerializeField] GameObject ResourceGooberPrefab;

    [SerializeField] int resources = 0;
    [SerializeField] int ID = 0;

    UIManager uiManager;

    public int Resources
    {
        get { return resources; }
        set
        {
            resources = value;
            OnResourceChanged.Invoke(0, resources);
        }
    }

    GameObject selector;

    private void Awake()
    {
        selector = (GameObject)Instantiate(SelectorPrefab);
        selector.GetComponent<PlayerSelector>().Init(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RepairPart(Scarecrow scarecrow, ScarecrowPart part)
    {
        int repairAmount = 1;
        if (Resources >= repairAmount)
        {
            scarecrow.RepairPart(part.type, repairAmount);
            Resources -= repairAmount;

            for (int i = 0; i < repairAmount; i++)
            {
                GameObject goober = (GameObject)Instantiate(ResourceGooberPrefab);
                goober.transform.position = uiManager.GetPlayerResourceBoxPosition(ID);
                Vector2 target = selector.transform.position;//part.transform.TransformPoint(part.transform.position);
                goober.GetComponent<ResourceGoober>().SetTarget(target);
            }
        }
    }
}
