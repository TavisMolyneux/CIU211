using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour 
{

	public GameObject Bullet;
	public GameObject BulletSpawn;
	public float Speed;


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Mouse0))
		{
			Fire ();
		}
	
	}

	void Fire()
	{
		var bullet = (GameObject)Instantiate (Bullet,BulletSpawn.transform.position,BulletSpawn.transform.rotation);

		bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * Speed;

		Destroy (bullet, 2.0f);
	}
}
