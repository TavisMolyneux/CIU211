using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavisBulletScript : MonoBehaviour 
{
	public float Lifetime;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{

		Lifetime -= Time.deltaTime;

		if (Lifetime <= 0) 
		{
			Destroy (gameObject);
		}


	}

	void OnCollisionEnter(Collision other)
	{
		print (other.gameObject.name);
		if(other.gameObject.tag==("Enemy"))
		{
			other.gameObject.GetComponent<Enemy>().health -= 1;

			Destroy (gameObject);
		}

		if (other.gameObject.tag ==("Player"))
		{
			other.gameObject.SendMessage ("TakeDMG", 1f);

		}
		Destroy(gameObject);
	
	}
}
