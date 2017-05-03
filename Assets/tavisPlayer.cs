using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tavisPlayer : MonoBehaviour 
{

	public float Health;
	public Image Blood;


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Health < 10) 
		{
			//Health += (Time.deltaTime * 0.1f);
		}

		if (Health <= 0)
		{
			Death ();
		}

		Blood.color = new Color(1, 1, 1, (((10-Health)/10)-0.1f));

	}

	void Death()
	{
		
	}

	void TakeDMG (float amt)
	{
		Health -= amt;
	}

}
