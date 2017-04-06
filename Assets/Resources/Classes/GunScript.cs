using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour
{
    public float fireRate = 1;
    private float maxTimer;
    public GameObject prefab;
    public bool fire;
    public Vector3 offSet;
    public bool loaded;
    public GameObject target;
    private AudioSource audioSource;

    void Start()
    {
        maxTimer = 1;
        fire = false;

        gameObject.AddComponent<AudioSource>().clip = Resources.Load("Audio/shoot") as AudioClip;
        audioSource = GetComponents<AudioSource>()[0];
    }

    void Update()
    {
        gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, GameObject.FindObjectOfType<CameraScript>().gameObject.transform.eulerAngles.x);
        if (maxTimer > 0)
        {
            loaded = false;
            maxTimer -= Time.deltaTime * fireRate;
        }else{
            loaded = true;
        }

        if (fire && loaded)
        {
            GameObject bullet = Instantiate(prefab, transform.position, transform.parent.rotation) as GameObject;
            bullet.transform.eulerAngles += new Vector3(GameObject.FindObjectOfType<CameraScript>().gameObject.transform.eulerAngles.x, 0, 0);
            //offSet = gameObject.transform.FindChild("Tip").transform.localToWorldMatrix*gameObject.transform.FindChild("Tip").transform.position;
            //Debug.Log(offSet);
            bullet.transform.position += offSet -transform.right*0.5f;
            bullet.transform.tag = gameObject.transform.tag;
            audioSource.Play(0);
            maxTimer = 1;
        }
    }
}
