using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] Text moneyText;
    float money = 0;

    GameObject background;
    GameObject backAnimationController;
    public GameObject house;
    public GameObject mainCamera;
    GameObject crane;
    public CraneController craneController;
    GameObject block;

    public static GameController instance;

    public string currentScin;
    public int currentHard;
    // Start is called before the first frame update
    void Start()
    {
        
            instance = this;

        GetSkin();
        CreateCrane();


        CrateHouse();
        InitCamera();
        CreateBackground();
        AddMoney(0);

    }

    void GetSkin()
    {
        if (PlayerPrefs.HasKey("currentScin"))
        {
            currentScin = PlayerPrefs.GetString("currentScin");
        }
        else
            currentScin = "Building1";
    }

    #region creatingObjects

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
            background.transform.position = new Vector3(0, -mainCamera.GetComponent<Camera>().orthographicSize, 0);
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
        GameObject g = Resources.Load<GameObject>("Scins\\" + currentScin + "\\house");

        if (g)
        {
            //house = new GameObject();

            house = Instantiate(g);
            house.name = "house";
            //house.transform.localScale = new Vector3(2.3f, 2.3f, 2.3f);

            Debug.Log(house.name + " is creating");
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

            Debug.Log(crane.name + " is creating");
        }

    }

    #endregion creatingObjects

    #region scenNavigation      

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene(1);
    }

    #endregion scenNavigation


    public void AddMoney(int x)
    {
        money += x;
        moneyText.text = money.ToString();
    }
}
