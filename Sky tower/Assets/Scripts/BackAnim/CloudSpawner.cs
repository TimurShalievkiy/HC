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
        if (Random.Range(0, 100) <= chance)
        {
            GameObject g = new GameObject();
            g.AddComponent<SpriteRenderer>();
            g.GetComponent<SpriteRenderer>().sprite = clouds[Random.Range(0, clouds.Length-1)];
            g.AddComponent<MoveToLineDirection>();
        }
    }

}
