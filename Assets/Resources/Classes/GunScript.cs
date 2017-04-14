using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GunScript : MonoBehaviour
{
    public float fireRate = 1;
    public float damage = 1;
    public float accuracy = 1;
    public int ammoCapacity = 6;
    public float reloadTime = 1;
    private float reloadTimer;
    private int ammoClip;
    private float maxTimer;
    public GameObject prefab;
    public bool fire;
    public Vector3 offSet;
    public bool loaded;
    public GameObject target;
    private AudioSource audioSource;
    private Vector3 normScale;
    private Vector3 currentScale;
    private GameObject crossHair;
    private bool reloading;

    void Start()
    {
        reloading = false;
        maxTimer = 1;
        fire = false;

        gameObject.AddComponent<AudioSource>().clip = Resources.Load("Audio/AutomaticRifle") as AudioClip;
        audioSource = GetComponents<AudioSource>()[0];

        crossHair = GameObject.Find("UICANVAS").transform.FindChild("Crosshair").gameObject;
        normScale = crossHair.transform.localScale;
        currentScale = normScale;
    }

    public void init()
    {
        ammoClip = ammoCapacity;
        crossHair = GameObject.Find("UICANVAS").transform.FindChild("Crosshair").gameObject;
        crossHair.transform.localScale *= (1.425f - accuracy);
        normScale = crossHair.transform.localScale;
        currentScale = normScale;

        GameObject.Find("UICANVAS").transform.FindChild("HuD (1)").FindChild("Mag Ammo").GetComponent<Text>().text = ammoClip.ToString();
    }

    void Update()
    {
        gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, GameObject.FindObjectOfType<Camera>().gameObject.transform.eulerAngles.x);
        if (maxTimer > 0)
        {
            loaded = false;
            maxTimer -= Time.deltaTime * fireRate;
        }else{
            loaded = true;
        }

        if(Input.GetKeyDown(KeyCode.R) && reloading == false)
        {
            reloading = true;
            reloadTimer = reloadTime;
        }

        if(reloadTimer > 0)
        {
            reloadTimer -= Time.deltaTime;
            if(reloadTimer <= 0)
            {
                reloadTimer = 0;
                ammoClip = ammoCapacity;
                reloading = false;
                GameObject.Find("UICANVAS").transform.FindChild("HuD (1)").FindChild("Mag Ammo").GetComponent<Text>().text = ammoClip.ToString();
            }
        }

        if (fire && loaded && ammoClip > 0)
        {
            ammoClip--;
            //float aimOffset = (1 - ((1 - (Random.Range(0f, 2f))) * ((1 + (crossHair.transform.localScale.x - normScale.x)*5) - (accuracy))));
            //Debug.Log(aimOffset + ":" + (crossHair.transform.localScale.x - normScale.x)*5);
            GameObject bullet = Instantiate(prefab, transform.position, transform.parent.rotation) as GameObject;
            bullet.transform.eulerAngles += new Vector3(GameObject.FindObjectOfType<Camera>().gameObject.transform.eulerAngles.x, 0, 0);
            bullet.transform.eulerAngles += new Vector3(generateRandomOffset(), generateRandomOffset(), generateRandomOffset());
            //offSet = gameObject.transform.FindChild("Tip").transform.localToWorldMatrix*gameObject.transform.FindChild("Tip").transform.position;
            //Debug.Log(offSet);
            bullet.transform.position += offSet -transform.right*0.5f;
            bullet.transform.tag = gameObject.transform.tag;
            //audioSource.Play(0);
            maxTimer = 1;

            GameObject shell = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            //shell = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cylinder), gameObject.transform.position + transform.up*0.25f, transform.rotation);
            shell.transform.position = transform.position + transform.up*0.15f;
            shell.transform.rotation = transform.rotation;
            shell.transform.localScale *= 0.025f;
            shell.AddComponent<Rigidbody>();
            shell.AddComponent<AudioSource>().clip = Resources.Load("Audio/AutomaticRifle") as AudioClip;
            AudioSource audioSrs = shell.GetComponents<AudioSource>()[0];
            audioSrs.Play(0);
            shell.transform.localEulerAngles += new Vector3(0, 0, 90);
            shell.GetComponent<Collider>().enabled = false;
            shell.GetComponent<Rigidbody>().velocity += (shell.transform.forward + shell.transform.right * 0.5f)*1;
            Destroy(shell, 0.75f);

            currentScale += new Vector3(damage/100, damage/100, damage/100);
            crossHair.transform.localScale += new Vector3(damage/100, damage/100, damage/100);

            if(currentScale.x > 0.5f)
            {
                currentScale = new Vector3(0.5f, 0.5f, 0.5f);
            }

            GameObject.Find("UICANVAS").transform.FindChild("HuD (1)").FindChild("Mag Ammo").GetComponent<Text>().text = ammoClip.ToString();
        }

        if(currentScale.x > normScale.x)
        {
            currentScale -= new Vector3(1, 1, 1)*Time.deltaTime;

            if(currentScale.x < normScale.x)
            {
                currentScale = normScale;
            }
        }
        crossHair.transform.localScale += new Vector3((currentScale.x-crossHair.transform.localScale.x), (currentScale.y-crossHair.transform.localScale.y), (currentScale.z-crossHair.transform.localScale.z))*Time.deltaTime*5;
        //crossHair.transform.localScale = currentScale;
    }

    private float generateRandomOffset()
    {
        return 1-(1-((1-(Random.Range(0f, 2f)))*((1+(crossHair.transform.localScale.x-normScale.x)*5)-(accuracy))));
    }
}
