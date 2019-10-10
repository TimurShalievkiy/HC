using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    bool isTouched = false;
    bool detouch = false;
    bool isPlased = false;
    public Rigidbody2D _rigidbody2d;
    public HingeJoint2D hinge;
    [SerializeField] GameObject particle;
    public Transform leftDot;
    public Transform rightDot;

    [SerializeField] Transform perfectObjects;
    static int currentIndexOfPerfectObjects = 0;

    float sizeForPerfect = 1;
    Vector3 scale;

    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        hinge = GetComponent<HingeJoint2D>();
 

        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).name == "PerfectObjects")
                perfectObjects = transform.GetChild(i);
        }
        scale = transform.localScale;
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        StartCoroutine(ScaleBlock());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (!isPlased )
        {

            if (collision.transform.tag == "startPlace")
            {
                Rigidbody2D r = collision.transform.GetComponent<Rigidbody2D>();
                if (r != null)
                {
                    CraneController.instance.CreateBlock(r);
                    hinge.connectedBody = r;
                    StartCoroutine(WhaitAndHinge());
                    if (CraneController.instance.listOfBlocks.Count >= 1)
                    {
    
                        if (transform.localPosition.x <= sizeForPerfect && transform.localPosition.x >= -sizeForPerfect)
                        {
                           // Debug.Log("Perfect11111");
                            particle.SetActive(true);
                            ShowPerfectObject();
                            CraneController.instance.pistonsController.IncrementCountOfPerfect();
                            GameController.instance.AddMoney(25);
                        }
                        else
                        {
                            CraneController.instance.pistonsController.ResetCountOfPerfect();
                        }

                    }

                }

                isTouched = true;
                isPlased = true;
            }

            if (!isTouched)
                if (collision.transform.tag == "block")
                {
                    _rigidbody2d.velocity = Vector2.zero;
                    isTouched = true;


                    if (transform.localPosition.x <= CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].transform.position.x + sizeForPerfect &&
                        transform.localPosition.x >= CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].transform.position.x - sizeForPerfect)
                    {

                        ShowPerfectObject();
                        CraneController.instance.pistonsController.IncrementCountOfPerfect();
                        GameController.instance.AddMoney(25);
                        particle.SetActive(true);
                    }
                    else
                    {
                        CraneController.instance.pistonsController.ResetCountOfPerfect();
                    }
                    


                    isPlased = true;

        
                    CraneController.instance.CreateBlock(collision.transform.GetComponent<Rigidbody2D>());
                    CraneController.instance.StopVelocity();

                    hinge.connectedBody = collision.transform.GetComponent<Rigidbody2D>();
                    StartCoroutine(WhaitAndHinge());

                }
        }

    }


    private void Update()
    {

        
        if (detouch)
        {

            if (transform.rotation.z > 0)
            {
                transform.eulerAngles -= new Vector3(0, 0, 1f);

                if (Vector3.Distance(transform.eulerAngles, Vector3.zero) <= 1)
                {
                    detouch = false;
                    transform.eulerAngles = Vector3.zero;

                    
                }
            }
            else
            {
                transform.eulerAngles += new Vector3(0, 0, 1f);
                if (Vector3.Distance(transform.eulerAngles, Vector3.zero) <= 1)
                {
                    detouch = false;
                    transform.eulerAngles = Vector3.zero;                  
                }

            }

        }
        if (!isPlased && CraneController.instance.listOfBlocks.Count > 1)
            if (Vector3.Distance(transform.position, CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].transform.position) <= 3f)
            {
                _rigidbody2d.velocity = Vector2.zero;
            }

    }

    public void Detouch()
    {

            transform.parent = null;
            detouch = true;
      
    }

    IEnumerator WhaitAndHinge()
    {
        yield return new WaitForSeconds(1f);
        // int index = CraneController.instance.rigidbody2Ds.IndexOf(this);

        hinge.enabled = true;
    }

    void ShowPerfectObject()
    {
        if (perfectObjects.childCount > 0)
        {
            if (perfectObjects.childCount <= currentIndexOfPerfectObjects)
            {
                currentIndexOfPerfectObjects = 0;
            }
            if (perfectObjects.childCount > 0)
                perfectObjects.GetChild(currentIndexOfPerfectObjects).gameObject.SetActive(true);
            currentIndexOfPerfectObjects++;

        }

    }

    IEnumerator ScaleBlock()
    {
        CraneController.isPause = true;

        while (Vector3.Distance(transform.localScale,scale)>0.05)
        {
            yield return new WaitForSeconds(0.02f);
            transform.localScale = Vector3.Lerp(transform.localScale, scale, 0.3f);
        }

        CraneController.isPause = false;
        StopCoroutine(ScaleBlock());
    }
}
