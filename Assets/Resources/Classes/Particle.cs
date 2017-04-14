using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Particle : MonoBehaviour
{
	void Start ()
    {
        gameObject.GetComponent<MeshRenderer>().material.color -= new Color(0, 0, 0, 0.75f);
        Destroy(gameObject, 0.25f);
	}
	
	void Update ()
    {
        gameObject.GetComponent<MeshRenderer>().material.color -= new Color(0, 0, 0, 7.5f/4)*Time.deltaTime;
	}
}
