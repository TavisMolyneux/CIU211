using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    public Rigidbody rb;
    public Rigidbody targetPosition;
    public bool targetFound;
    public bool setDown;
    private int range;
    private float shootTimer;
    public int health = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetFound = false;
        setDown = true;
        range = 15;
        shootTimer = 1;
    }

    void Update ()
    {

        //transform.LookAt(target.transform.position);

        if(health < 0)
        {
            Destroy(gameObject);
        }
        gameObject.transform.FindChild("Pistol").gameObject.GetComponent<npcgun>().fire = false;
        target = GameObject.FindGameObjectWithTag("Player");
        if (setDown && Time.timeScale > 0)
        {
            if (target != null && Vector3.Distance(target.transform.position, gameObject.transform.position) < range)
            {
                gameObject.transform.FindChild("Pistol").gameObject.GetComponent<npcgun>().fire = true;
                targetPosition = target.GetComponent<Rigidbody>();
                float setrotation = (Mathf.Atan2(targetPosition.position.z - rb.position.z, targetPosition.position.x - rb.position.x) * 180 / Mathf.PI) - 90; //Finds the true bearing from this object, to its target.
                transform.rotation = Quaternion.Euler(0, -setrotation, 0);
            }
            else if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                target = GameObject.FindGameObjectWithTag("Player");
                targetPosition = target.GetComponent<Rigidbody>();
            }
        }
    }

    /*public GameObject GetTarget()
    {
        return target;
    }*/
}