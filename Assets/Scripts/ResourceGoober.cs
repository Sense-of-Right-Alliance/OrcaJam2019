using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGoober : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    Vector2 target;
    Vector2 start;

    float i;
    float rate = 1f;

    float rotateDir = 1f;

    public void SetTarget(Vector2 target)
    {
        this.target = target;
        start = transform.position;

        i = 0;

        rotateDir = Random.value > 0.5f ? 1f : -1f;

        float distance = Mathf.Abs((target - start).magnitude);

        rate = speed / distance;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.Rotate(Vector3.forward * 1000f * rotateDir * Time.deltaTime);

            Vector3 newPos = Vector2.Lerp(start, target, i);
            i += rate * Time.deltaTime;

            newPos.z = -5;

            transform.position = newPos;
            if (i >= 1) Destroy(gameObject);
        }
    }
}
