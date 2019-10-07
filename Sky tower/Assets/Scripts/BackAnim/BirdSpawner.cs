using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    GameObject bird;
    [SerializeField] float minSpeed = 1;
    [SerializeField] float maxSpeed = 1;
    [SerializeField] float chance = 1;
    [SerializeField] bool isLeftDirection = true;



   
   public float maxY ;

    private void Start()
    {
        maxY = 80;
        //  maxY = GameController.instance.mainCamera.transform.position.y + GameController.instance.mainCamera.GetComponent<Camera>().orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) <= chance)
        {
            GameObject g = Resources.Load<GameObject>("BackObjects\\Bird\\Bird");
            g = Instantiate(g);

            if (g != null)
            {
        
                g.transform.position = new Vector3(transform.position.x, Random.Range(transform.position.y, maxY));

                g.GetComponent<MoveToLineDirection>().ChangeSpeed(minSpeed, maxSpeed);
                g.transform.parent = transform;

                if (isLeftDirection)
                    g.transform.localScale = new Vector3(-1, 1, 1);
                else
                    g.transform.localScale = new Vector3(1, 1, 1);

                Destroy(g, 5f);
                if (Camera.main.transform.position.y > maxY + 10)
                    Destroy(this.gameObject);
            }
        }
    }
}
