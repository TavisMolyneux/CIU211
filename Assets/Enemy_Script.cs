using UnityEngine;
using System.Collections;

public class Enemy_Script : MonoBehaviour
{

	public float Health;
	public float Speed;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	

		if (Health <= 0)
		{
			Death ();
		}

	}

	void Death ()
	{
		Destroy (this.gameObject);
	}

	void Damage(float amt)
	{
		Health = Health - amt;
		print ("got hit" + Health);
	}
}
