using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    GameObject background;
    GameObject backAnimationController;
    public GameObject house;
    GameObject mainCamera;
    GameObject crane;
    public CraneController craneController;
    GameObject block;

    public static GameController instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;

        CreateCrane();


        CrateHouse();
        InitCamera();
        CreateBackground();
       

    }

    void CreateBackground()
    {
        Sprite[] g = Resources.LoadAll<Sprite>("Background");
        if (g.Length > 0)
        {
            
            background = new GameObject();
            background.name = "background";
            background.AddComponent<SpriteRenderer>();
            background.GetComponent<SpriteRenderer>().sprite = g[0];
            background.GetComponent<SpriteRenderer>().sortingOrder = -100;
            background.transform.localScale = new Vector3(8, 8, 8);
            background.transform.position = new Vector3(0,-mainCamera.GetComponent<Camera>().orthographicSize,0);
        }
    }
    void InitCamera()
    {
        mainCamera = Camera.main.gameObject;
        if (mainCamera.GetComponent<CameraController>() == null)
        {
            mainCamera.AddComponent<CameraController>();
            
        }
        mainCamera.GetComponent<CameraController>().InitCamera(crane.transform, house.transform);
        house.transform.position = new Vector3(0, -mainCamera.GetComponent<Camera>().orthographicSize, 0);
    }
    void CrateHouse()
    {
        GameObject g = Resources.Load<GameObject>("Scins\\Building1\\house");
        if (g)
        {
            //house = new GameObject();
            
            house = Instantiate(g);
            house.name = "house";
            house.transform.localScale = new Vector3(2, 2, 2);
            
            Debug.Log(house.name);
        }
    }

    void CreateCrane()
    {
        GameObject g = Resources.Load<GameObject>("Crane");
        if (g)
        {
            //house = new GameObject();

            crane = Instantiate(g);
            crane.name = "crane";
            crane.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            
            Debug.Log(crane.name);
        }

    }
}
