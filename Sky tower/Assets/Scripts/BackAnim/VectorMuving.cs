using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorMuving : MonoBehaviour
{
    [SerializeField] string path ;
    [SerializeField] Vector2 direction ;
    [SerializeField] float minSpeed = 1;
    [SerializeField] float maxSpeed = 1;
    [SerializeField] float chance = 1;

    [SerializeField] float deadTime = 5;





    public float maxY;
    public float miny;
    // Update is called once per frame
    void Update()
    {
        if(GameController.instance.mainCamera.transform.position.y+10<miny)
        if (Random.Range(0, 100) <= chance)
        {
            GameObject[] list = Resources.LoadAll<GameObject>("BackObjects\\" + path);
            GameObject g = Instantiate(list[Random.Range(0,list.Length)]);

            if (g != null)
            {

                g.transform.position += new Vector3(transform.position.x, Random.Range(transform.position.y, maxY));

                g.GetComponent<MoveToLineDirection>().ChangeSpeed(minSpeed, maxSpeed);
                g.transform.parent = transform;

         
                Destroy(g, deadTime);

                if (Camera.main.transform.position.y > maxY + 10)
                    Destroy(this.gameObject);
            }
        }
    }
}
