using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage;

	void Start ()
    {
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward*500, ForceMode.VelocityChange);
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        Destroy(gameObject, 1);
    }
	void Update ()
    {
		
	}

    void OnCollisionEnter()
    {
        Destroy(gameObject);
    }
}
