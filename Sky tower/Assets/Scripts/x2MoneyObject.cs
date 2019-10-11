using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class x2MoneyObject : MonoBehaviour
{
    [SerializeField] Vector2 direction;
    [SerializeField] float minSpeed = 1;
    [SerializeField] float maxSpeed = 1;

    [SerializeField] float minDelay = 1;
    [SerializeField] float maxDelay = 1;
    [SerializeField] float currentDelay = 1;

    [SerializeField] float deadTime = 5;
    [SerializeField] float timeForActivation ;
    [SerializeField] float currentTimeForActivation;


    public static float currentScale = 1;

    bool isactive = false;
    private void Start()
    {
        currentDelay = Random.Range(minDelay, maxDelay);
        currentScale = 1;
    }

    private void Update()
    {
        if (isactive)
        {
            if (currentTimeForActivation <= 0)
            {
                currentScale = 1;
                currentDelay = Random.Range(minDelay, maxDelay);
                isactive = false;
            }
            else
                currentTimeForActivation -= Time.deltaTime;

        }
        if (currentDelay <= 0 && currentTimeForActivation <= 0)
        {
            CreateObject();

            currentDelay = Random.Range(minDelay, maxDelay);
        }
        else
        {
            currentDelay -= Time.deltaTime;
        }
    }
    // Start is called before the first frame update

    void CreateObject()
    {
        //GameObject[] list = Resources.LoadAll<GameObject>("BackObjects\\x2" );
        GameObject g = Resources.Load<GameObject>("BackObjects\\x2");


        if (g != null)
        {

            
            g = Instantiate(g);
            g.transform.position = new Vector3(Random.Range(transform.position.x-Screen.width / 3 , transform.position.x+ Screen.width / 3), Screen.height);
            float x = (float)Screen.width / 7;
            Debug.Log(x);
            g.GetComponent<RectTransform>().sizeDelta = new Vector2(x, x);
            g.GetComponent<Button>().onClick.AddListener(TouchObject); 

            g.transform.localScale = new Vector3(1, 1, 1);
            g.transform.parent = transform;
            g.GetComponent<MoveByVector>().ChangeSpeed(direction.x, direction.y, Screen.height / 15, Screen.height / 10, deadTime);





        }
    }
    public void TouchObject()
    {
        Debug.Log(11111111111);
        currentScale = 2;
        currentTimeForActivation = timeForActivation;
        isactive = true;
    }
}
