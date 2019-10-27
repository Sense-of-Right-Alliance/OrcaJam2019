using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSwarmVisual : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    Vector2 target;
    Vector2 start;

    float i;
    float rate = 1f;

    float rotateDir = 1f;

    int state = -1;

    public void SetTarget(Vector2 target)
    {
        this.target = target;
        start = transform.position;

        i = 0;

        rotateDir = Random.value > 0.5f ? 1f : -1f;

        float distance = Mathf.Abs((target - start).magnitude);

        rate = speed / distance;

        state++;
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
            if (state == 1)
            {
                i += Time.deltaTime * 2f;
            }
            else
            {
                Vector3 newPos = Vector2.Lerp(start, target, i);
                i += rate * Time.deltaTime;

                newPos.z = -5;

                transform.position = newPos;
            }

            transform.Rotate(Vector3.forward * 1000f * rotateDir * Time.deltaTime);

            if (i >= 1)
            {
                if (state == 0)
                {
                    state++;
                    i = 0;
                } 
                else if (state == 1)
                {
                    SetTarget(start);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
           
        }
    }
}
