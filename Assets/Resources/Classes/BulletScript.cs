using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage;
    public float velocity;
    private float gravTimer = 0.5f;
    private Vector3 oldPos;

	void Start ()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        Destroy(gameObject, 1);
    }

    public void init()
    {
        gameObject.transform.eulerAngles += new Vector3(0.1f, 0, 0);
        gameObject.GetComponent<Rigidbody>().AddForce(-gameObject.transform.up * velocity, ForceMode.VelocityChange);
    }

	void Update ()
    {
        int spawnCounter = 0;
        while (spawnCounter < Vector3.Distance(transform.position, oldPos)/5)
        {
            GameObject vectorParticle = Instantiate(Resources.Load("Prefabs/3DParticle"), gameObject.transform.position + transform.up*spawnCounter*5, transform.rotation) as GameObject;
            vectorParticle.transform.localScale = new Vector3(vectorParticle.transform.localScale.x, 5, vectorParticle.transform.localScale.z);
            /*if(spawnCounter+1 >= Vector3.Distance(transform.position, oldPos) / 5)
            {
                vectorParticle.transform.localScale = new Vector3(vectorParticle.transform.localScale.x, 2.5f, vectorParticle.transform.localScale.z);
                vectorParticle.transform.position -= transform.up * 2.5f;
            }*/
            spawnCounter++;
        }
        oldPos = transform.position;

        if (gravTimer > 0)
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

    void OnCollisionEnter(Collision other)
    {
//        if(other.gameObject.GetComponent<Enemy>())
//        {
//            other.gameObject.GetComponent<Enemy>().health -= 1;
//        }

        if (other.gameObject.GetComponent<PlayerScript>())
        {
            other.gameObject.GetComponent<PlayerScript>().health -= 1;
        }
        Destroy(gameObject);
    }
}
