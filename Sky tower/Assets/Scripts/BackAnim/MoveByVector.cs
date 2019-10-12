using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByVector : MonoBehaviour
{
    [SerializeField] float minSpeed = 1;
    [SerializeField] float maxSpeed = 3;
    [SerializeField] float currentSpeed = 0;
    Vector2 direction;
    float lifeTime = 0;
    Vector3 scale;

    public void ChangeSpeed(float x, float y,float min,float max, float time)
    {
        direction.x = x;
        direction.y = y;
        currentSpeed = Random.Range(min, max);
        scale = transform.localScale;
        transform.localScale = new Vector3(0f, 0f, 0f);
        lifeTime = time;
        
   
    }
    // Update is called once per frame

    private void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime >= 0 && Vector3.Distance(transform.localScale, scale) > 0.1f )
        {
            transform.localScale = Vector3.Lerp(transform.localScale, scale, 0.05f);
        }

        transform.position += new Vector3(direction.x * currentSpeed * Time.deltaTime, direction.y * currentSpeed * Time.deltaTime, 0);

        if (lifeTime <= 0)
        {
            if (Vector3.Distance(transform.localScale, new Vector3(0, 0, 0)) > 0.1f)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), 0.05f);
            }
            else
            {
                Destroy(gameObject);
            }
               
        }
    }

    public void DestoryObject()
    {
        lifeTime = 0;
        
    }
   
}
