using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    //скорость шайбы
    public float speed = 1;

	// Use this for initialization
	void Start () {
        //инициализируем значение скорости
        speed = JsonFileWriter.data.ballSpeed;

        //задаем базовое ускорение
        this.transform.GetComponent<Rigidbody>().velocity = Vector3.back * speed;
        
    }
    void Update()
    {
       //если магнитуда не равна скорости
        if (this.transform.GetComponent<Rigidbody>().velocity.magnitude != speed)
        {   //то возвращаем значение ускорения 
            this.transform.GetComponent<Rigidbody>().velocity = this.transform.GetComponent<Rigidbody>().velocity.normalized * speed;
         
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //если шайба соприкоснулась с ракеткой
        if (collision.transform.GetComponent<RocketController>() != null)
        {
            //получаем направление в зависимости от места в которое ударилась шайба
            float vx = HitFactor(this.transform.position, collision.transform.position, collision.collider.bounds.size.x);
            //создаем вектор направления
            Vector3 dir = new Vector3(vx, 0f, -1f);
            //задаем направление и ускорение
            this.transform.GetComponent<Rigidbody>().velocity = dir * speed;
        }
        else
        {
            //this.transform.GetComponent<Rigidbody>().velocity = this.transform.GetComponent<Rigidbody>().velocity * speed * Time.deltaTime;
        }
    }


    //метод получения направления в зависимости от места удара шайбы по ракетке
    float HitFactor(Vector3 ballPos, Vector3 rocketPos, float rocketHeight)
    {      
        return ( ballPos.x - rocketPos.x) / rocketHeight;
    }

}
