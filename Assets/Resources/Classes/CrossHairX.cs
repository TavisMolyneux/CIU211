using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHairX : MonoBehaviour
{
    GameObject[] hairs;
    public Sprite quartHair;

	void Start ()
    {
        hairs = new GameObject[4];
        
        for(int i = 0; i < hairs.Length; i++)
        {
            hairs[i] = new GameObject();
            hairs[i].AddComponent<Image>().sprite = quartHair;
            hairs[i].transform.parent = GameObject.FindObjectOfType<Canvas>().gameObject.transform;
            //Instantiate(hairs[i], GameObject.Find("UICANVAS").transform.FindChild("Crosshair").GetChild(i).transform.position, GameObject.Find("UICANVAS").transform.FindChild("Crosshair").GetChild(i).transform.rotation);
            //hairs[i] = Instantiate(hairs[i], gameObject.transform.GetChild(i).transform.position, gameObject.transform.GetChild(i).transform.rotation) as GameObject;
            hairs[i].transform.rotation = gameObject.transform.GetChild(i).transform.rotation;
            hairs[i].layer = gameObject.layer;
            hairs[i].GetComponent<Image>().SetNativeSize();
            hairs[i].transform.localScale *= 0.5f;
        }
	}

	void Update ()
    {
        for (int i = 0; i < hairs.Length; i++)
        {
            //hairs[i].transform.position = GameObject.Find("UICANVAS").transform.FindChild("Crosshair").GetChild(i).transform.position;
            hairs[i].transform.position = gameObject.transform.GetChild(i).transform.position;
        }
    }
}
