using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {

	public bool PauseActive;
	public GameObject PausePanel;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyUp (KeyCode.Escape))
		{
			Pause ();
		}


	}

	public void Pause ()
	{
		PauseActive = !PauseActive;
		GameObject Controller = GameObject.FindGameObjectWithTag ("Player");
		Controller.GetComponent<PlayerScript> ().enabled = PauseActive;
		GameObject.FindObjectOfType<Camera>().GetComponent<Camera>().enabled = PauseActive;
		PausePanel.SetActive (!PauseActive);
	}

	public void quitfunction ()
	{
		Application.Quit ();
	}
}
