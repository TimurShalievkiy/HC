using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    Sprite[] clouds;
    [SerializeField]float chance = 1;
    // Start is called before the first frame update
    void Start()
    {
        clouds = Resources.LoadAll<Sprite>("Clouds");       
    }

    private void Update()
    {
        if (Camera.main.transform.position.y > 50 && Random.Range(0, 100) <= chance)
        {
            GameObject g = new GameObject();
            g.transform.position = new Vector3(12, Random.Range(Camera.main.transform.position.y - 20.0f, Camera.main.transform.position.y + 60.0f));
            g.AddComponent<SpriteRenderer>();
            g.GetComponent<SpriteRenderer>().sprite = clouds[Random.Range(0, clouds.Length)];
            g.AddComponent<MoveToLineDirection>();
            g.GetComponent<MoveToLineDirection>().ChangeSpeed(1.0f, 3.0f);
            g.transform.parent = transform;
            g.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            Destroy(g, 30f);
        }
    }

}
