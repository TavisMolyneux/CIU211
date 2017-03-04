using UnityEngine;
using System.Collections;

public class Bullet_script : MonoBehaviour 
{
	public bool Speed;
	public bool Damage;
	public ParticleSystem Blood;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnCollisionEnter(Collision collison)
	{
		if (collison.gameObject.tag == "Enemy") 
		{
			print ("hit");
			collison.gameObject.SendMessage ("Damage", 10f);
			Instantiate (Blood);
		}

		Destroy (this.gameObject);

	}
}
