﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    GameObject g;
    [SerializeField]AtackListener atackListener;


    // Start is called before the first frame update
    void Start()
    {
        g= Resources.Load<GameObject>("particles");       
        atackListener = transform.GetComponent<AtackListener>();      
        g = Instantiate(g, transform);
        g.transform.localPosition = Vector3.zero;
        particle = g.GetComponent<ParticleSystem>();

        atackListener.OnAtacked += delegate { ShowParticles(); };
    }


    public void ShowParticles()
    {
        Debug.Log("Show Particles");
        
        particle.Play();
    }
}
