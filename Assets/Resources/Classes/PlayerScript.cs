using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float agility;//Acceleration[100%] | Deceleration[50%] _speed
    private float legStrength;//Jump height + movement speed (jump height = 33.3...% legStrength | walk speed = 50% legStrength | run speed = 100% legStrength)

    private float activateRunTimer;
    private bool running;
    private float speed;
    private bool moving;

    private Rigidbody body;
    private GameObject gun;

    void Start ()
    {
        //Statistics
        agility = 50;
        legStrength = 10;

        //Misc
        activateRunTimer = 0;
        running = false;
        moving = false;

        //Main_Appliments
        gameObject.tag = "Player";
        gameObject.layer = 0;

        //Components
        gameObject.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        //Component References
        body = gameObject.GetComponent<Rigidbody>();
        gun = gameObject.transform.FindChild("WPN_AKM").gameObject;

        ///Manual_Static-Initialization
		Camera.init();
    }

    void Update()
    {
        RayCasts();
        timers();

        if (Input.GetMouseButtonDown(0))
        {
            gun.GetComponent<GunScript>().fire = true;
        }else if (Input.GetMouseButtonUp(0))
        {
            gun.GetComponent<GunScript>().fire = false;
        }

        if (Input.anyKey)
        {
            keyDetection();
            if(Input.GetKeyDown(KeyCode.W))
            {
                if(activateRunTimer > 0)
                {
                    running = true;
                }else{
                    activateRunTimer = 0.25f;
                    running = false;
                }
            }
        }else{
            moving = false;
        }

        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            running = false;
        }

        float[] duelVelocity = new float[2];
        duelVelocity[0] = body.velocity.x;
        duelVelocity[1] = body.velocity.z;
        for(int i = 0; i < duelVelocity.Length; i++)
        {
            if(duelVelocity[i] > speed)
            {
                duelVelocity[i] = speed;
            }else if(duelVelocity[i] < -speed)
            {
                duelVelocity[i] = -speed;
            }

            if(!moving && Physics.Raycast(body.position, -transform.up, 1, 1))
            {
                var deceleration = (agility/2)*Time.deltaTime;
                if(duelVelocity[i] - deceleration > 0)
                {
                    duelVelocity[i] -= deceleration;
                }else if(duelVelocity[i] + deceleration < 0)
                {
                    duelVelocity[i] += deceleration;
                }else{
                    duelVelocity[i] = 0;
                }
            }
        }

        body.velocity = new Vector3(duelVelocity[0], body.velocity.y, duelVelocity[1]);
        body.transform.eulerAngles += new Vector3(0, Input.GetAxis("Mouse X"), 0);
        //transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * 25);
    }

    private void keyDetection()
    {
        speed = legStrength/2;
        if(running)
            speed = legStrength;

        var acceleration = agility * Time.deltaTime;

        if(Input.GetKey(KeyCode.W))
            body.velocity += new Vector3(Mathf.Sin(this.transform.eulerAngles.y*Mathf.PI/180), 0, Mathf.Cos(this.transform.eulerAngles.y*Mathf.PI/180))*acceleration; moving = true;

        if(Input.GetKey(KeyCode.A))
            body.velocity += -transform.right * acceleration; moving = true;

        if(Input.GetKey(KeyCode.S))
            body.velocity += -transform.forward * acceleration; moving = true;

        if(Input.GetKey(KeyCode.D))
            body.velocity += transform.right * acceleration; moving = true;

        if(Input.GetKeyDown(KeyCode.Space))
            if(Physics.Raycast(body.position, -transform.up, 1, 1))
                body.velocity += new Vector3(0, legStrength/3, 0);
    }

    private void timers()
    {
        if(activateRunTimer > 0)
        {
            activateRunTimer -= Time.deltaTime;
            if(activateRunTimer < 0)
                activateRunTimer = 0;
        }
    }

    private void RayCasts()
    {
        Debug.DrawRay(body.position, -transform.up * 1, Color.red);
    }
}
