using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoVisual : HazySeasonVisualEffect
{
    [SerializeField] private GameObject tornadoPrefab;
    [SerializeField] private GameObject explosionPrefab;

    float minX = -10;
    float maxX = 10;
    float minY = -10;
    float maxY = 10;

    TornadoDirection _direction;
    float speed = 8f;

    List<ScarecrowPart> _parts;

    public void Init(float duration, TornadoDirection direction, List<ScarecrowPart> parts)
    {
        base.Init(duration);

        Vector3 start = Vector3.zero;
        Vector3 end = Vector3.zero;

        _direction = direction;
        _parts = parts;

        StartCoroutine(CreateExplosions());


        if (direction == TornadoDirection.FrontToBack)
        {
            start = new Vector3(minX, 4, 10);
            end = new Vector3(-8, 3, 10);

            speed = ((end - start).magnitude * 3f) / duration;

            CreateTornado(start, end, TornadoDirection.LeftToRight);
            
            start = new Vector3(maxX, 4, 10);
            end = new Vector3(8, 3, 10);

            speed = ((end - start).magnitude * 3f) / duration;

            CreateTornado(start, end, TornadoDirection.RightToLeft);
        }
        else
        {
            if (direction == TornadoDirection.LeftToRight)
            {
                start = new Vector3(minX, 4, 10);
                end = new Vector3(-8, 3, 10);
            }
            else if (direction == TornadoDirection.RightToLeft)
            {
                start = new Vector3(maxX, 4, 10);
                end = new Vector3(8, 3, 10);
            }

            speed = ((end - start).magnitude * 3f) / duration;

            CreateTornado(start, end, _direction);
        }
    }

    private void CreateTornado(Vector3 start, Vector3 end, TornadoDirection direction)
    {
        GameObject tornado = Instantiate(tornadoPrefab);
        tornado.transform.position = start;
        tornado.GetComponent<Tornado>().speed = speed;
        tornado.GetComponent<Tornado>().easing = 1;
        tornado.GetComponent<Tornado>().SetTarget(end);
    }

    private IEnumerator CreateExplosions()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < _parts.Count; i++)
        {
            yield return new WaitForSeconds(0.1f);
            Vector3 pos = new Vector3(_parts[i].transform.position.x, _parts[i].transform.position.y, -5);
            Instantiate(explosionPrefab, pos, Quaternion.identity);
        }
    }
}
