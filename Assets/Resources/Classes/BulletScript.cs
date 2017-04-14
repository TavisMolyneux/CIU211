using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage;
    public float velocity;
    private float gravTimer = 0.5f;

	void Start ()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        Destroy(gameObject, 5);
    }

    public void init()
    {
        gameObject.transform.eulerAngles += new Vector3(0.1f, 0, 0);
        gameObject.GetComponent<Rigidbody>().AddForce(-gameObject.transform.up * velocity, ForceMode.VelocityChange);
    }

	void Update ()
    {
        if(gravTimer > 0)
        {
            gravTimer -= Time.deltaTime;

            if(gravTimer < 0)
            {
                gravTimer = 0;
                gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
        }
        //gameObject.transform.position += -transform.up * velocity * Time.deltaTime;
        gameObject.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity - gameObject.GetComponent<Rigidbody>().velocity*0.25f*Time.deltaTime;
    }

    void OnCollisionEnter()
    {
        Destroy(gameObject);
    }
}
