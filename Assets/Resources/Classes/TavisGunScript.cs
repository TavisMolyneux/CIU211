using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TavisGunScript : MonoBehaviour 
{

	public GameObject spawn;
	public GameObject Bullet;
	public float Speed;
	public float MagAmount;
	public float MagLeft;
	public GameObject CurMag;
	public GameObject MagsLeft;
	public float Reloadtime;
	public bool Reloading;


	// Use this for initialization
	void Start ()
	{
		
	}

	// Update is called once per frame
	void Update () 
	{
		CurMag.gameObject.GetComponent<Text> ().text = MagAmount.ToString();
		MagsLeft.gameObject.GetComponent<Text> ().text = MagLeft.ToString ();

		if (Input.GetButtonDown ("Fire1")) 
		{
			if (MagAmount > 0)
			{
				Shoot ();
			} 

			if (MagAmount <= 0 && Reloading != true)
			{
				StartCoroutine (Reload ());
			}
		}
	}

	void Shoot ()
	{
		MagAmount -= 1;
		RaycastHit hit;
		Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0.5f));

		if (Physics.Raycast (ray, out hit)) 
		{
			
		}
		spawn.gameObject.transform.LookAt (hit.point);

		GameObject bullet = Bullet;
		bullet = (GameObject)Instantiate (Bullet, spawn.transform.position, spawn.transform.rotation);
		//bullet.transform.LookAt (hit.point);
		//bullet.GetComponent<Rigidbody> ().AddForce (transform.forward * Speed);
		bullet.GetComponent<Rigidbody> ().AddForce ((hit.point - transform.position).normalized * Speed);
	



	}

	IEnumerator Reload ()
	{	
		Reloading = true;
		yield return new WaitForSeconds (2);
		MagAmount = 30;
		MagLeft -= 1;
		Reloading = false;
		yield return null;
		
	}
		
}
