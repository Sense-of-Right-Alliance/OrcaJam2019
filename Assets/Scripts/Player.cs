using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public class ResourceChangedEvent : UnityEvent<int, int> { }
    public static ResourceChangedEvent OnResourceChanged { get; } = new ResourceChangedEvent();

    [SerializeField] GameObject SelectorPrefab;

    [SerializeField] int resources = 0;
    [SerializeField] int ID = 0;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
