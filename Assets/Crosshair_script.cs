using UnityEngine;
using System.Collections;

public class Crosshair_script : MonoBehaviour 
{

	public bool CrosshairOn;
	public GameObject crosshair;

	// Use this for initialization
	void Start () 
	{
		
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		crosshair.SetActive (CrosshairOn);
	
	}
}
