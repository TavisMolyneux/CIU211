using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	void Start ()
    {
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward*250, ForceMode.VelocityChange);
        //Destroy(gameObject, 1);
	}
	void Update ()
    {
		
	}
}
