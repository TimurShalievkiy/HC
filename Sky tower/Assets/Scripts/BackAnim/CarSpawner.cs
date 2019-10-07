using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{

    Sprite[] cars;
    [SerializeField] float minSpeed = 1;
    [SerializeField] float maxSpeed = 1;
    [SerializeField] int order = 5;
    [SerializeField] bool isLeftDirection = true;
    // Start is called before the first frame update

    Sprite buff;
    void Start()
    {
        cars = Resources.LoadAll<Sprite>("BackObjects\\Cars");
        StartCoroutine(CreateCar());
    }

    IEnumerator CreateCar()
    {
        Debug.Log(1);
        while (GameController.instance.mainCamera.transform.position.y < 40)
        {
            yield return new WaitForSeconds(Random.Range(2f, 3f));

            GameObject g = new GameObject();
            g.AddComponent<SpriteRenderer>();

            buff = cars[Random.Range(0, cars.Length)];
   
            g.GetComponent<SpriteRenderer>().sprite = buff;
            g.GetComponent<SpriteRenderer>().sortingOrder = order;
            g.AddComponent<MoveToLineDirection>();

            g.transform.parent = transform;
            g.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);



            g.transform.localPosition = new Vector3(transform.position.x, 0);

            g.GetComponent<MoveToLineDirection>().ChangeSpeed(minSpeed, maxSpeed);
            g.transform.parent = transform;

            if (isLeftDirection)
                g.transform.localScale = new Vector3(-1, 1, 1);
            else
                g.transform.localScale = new Vector3(1, 1, 1);


            if (buff.name == "car1" || buff.name == "car2" || buff.name == "car3")
                g.AddComponent<CarTouchController>();

            Destroy(g, 10f);


        }
    }
}
