using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class npcgun : MonoBehaviour
{
    private float shootTimer = 1;
    public GameObject prefab;
    public bool fire;

    void Start()
    {

    }

    void Update()
    {
        if(shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }

        if(fire && shootTimer <= 0)
        {
			Instantiate(prefab, transform.position, transform.rotation).GetComponent<Rigidbody>().AddForce(transform.forward * 3000);
            shootTimer = 1;
        }
    }
}
