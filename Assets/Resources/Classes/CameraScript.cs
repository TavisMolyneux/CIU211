using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript: MonoBehaviour
{
    private static GameObject player;
	void Start ()
    {

	}

    public static void init()
    {
        Cursor.visible = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	void Update ()
    {
        if (player != null)
        {
            this.transform.position = player.transform.position + new Vector3(0, 0.5f, 0);
            this.transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), 0, 0);
            this.transform.eulerAngles = new Vector3(transform.eulerAngles.x, player.transform.eulerAngles.y, player.transform.eulerAngles.z);
        }
	}
}
