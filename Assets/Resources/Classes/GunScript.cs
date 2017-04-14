using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GunScript : MonoBehaviour
{
    public float fireRate = 1;
    public float damage = 1;
    public float accuracy = -1;
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
    private Vector3 normScale;
    private Vector3 currentScale;
    private GameObject crossHair;
    private bool reloading;
    private float handling;

    void Start()
    {
        handling = 0;
        ammoClip = ammoCapacity;
        reloading = false;
        maxTimer = 1;
        fire = false;

        crossHair = GameObject.Find("UICANVAS").transform.FindChild("Crosshair").gameObject;
        if (accuracy != -1)
        {
            crossHair.transform.localScale *= (1.425f - accuracy);
        }
        normScale = crossHair.transform.localScale;
        currentScale = normScale;

        GameObject.Find("UICANVAS").transform.FindChild("HuD (1)").FindChild("Mag Ammo").GetComponent<Text>().text = ammoClip.ToString();
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
        gameObject.transform.eulerAngles = new Vector3(GameObject.FindObjectOfType<Camera>().gameObject.transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
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

        if (fire && loaded && ammoClip > 0 && !reloading)
        {
            ammoClip--;
            GameObject bullet = prefab;
            bullet = Instantiate(prefab, transform.FindChild("ExitPoint").position, transform.parent.rotation) as GameObject;
            bullet.transform.eulerAngles += new Vector3(GameObject.FindObjectOfType<Camera>().gameObject.transform.eulerAngles.x + prefab.transform.eulerAngles.x, 0, 0);
            bullet.transform.eulerAngles += new Vector3(generateRandomOffset(), generateRandomOffset(), generateRandomOffset());
            bullet.transform.tag = gameObject.transform.tag;
            bullet.GetComponent<BulletScript>().init();
            maxTimer = 1;

            ejectShell();

            handling += damage;
            currentScale += new Vector3(damage/100, damage/100, damage/100);
            crossHair.transform.localScale += new Vector3(damage/100, damage/100, damage/100);

            if(currentScale.x > normScale.x + 0.3f)
            {
                currentScale = new Vector3(normScale.x + 0.3f, normScale.x + 0.3f, normScale.x + 0.3f);
            }

            GameObject.Find("UICANVAS").transform.FindChild("HuD (1)").FindChild("Mag Ammo").GetComponent<Text>().text = ammoClip.ToString();

            Debug.Log((fireRate / (1 + handling / damage)) / 4);
        }

        if (handling > 0)
        {
            handling -= damage*fireRate*Time.deltaTime/2;
            if(handling < 0)
            {
                handling = 0;
            }
        }

        float recoilControll = (fireRate / (1 + handling / damage))/4;

        if(currentScale.x > normScale.x)
        {
            currentScale -= new Vector3(1, 1, 1)*Time.deltaTime*recoilControll;

            if(currentScale.x < normScale.x)
            {
                currentScale = normScale;
            }
        }
        crossHair.transform.localScale += new Vector3((currentScale.x-crossHair.transform.localScale.x), (currentScale.y-crossHair.transform.localScale.y), (currentScale.z-crossHair.transform.localScale.z))*Time.deltaTime*5;
    }

    private float generateRandomOffset()
    {
        return 1-(1-((1-(Random.Range(0f, 2f)))*((1+(crossHair.transform.localScale.x-normScale.x)*5)-(accuracy))));
    }

    private void ejectShell()
    {
        GameObject shell = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        shell.transform.position = transform.position + transform.up * 0.1f + transform.forward*0.025f;
        shell.transform.rotation = transform.rotation;
        shell.transform.localScale *= 0.02f;
        shell.AddComponent<Rigidbody>();
        shell.AddComponent<AudioSource>().clip = Resources.Load("Audio/AutomaticRifle") as AudioClip;
        AudioSource audioSrs = shell.GetComponents<AudioSource>()[0];
        audioSrs.Play(0);
        shell.transform.localEulerAngles += new Vector3(90, 0, 0);
        shell.GetComponent<Collider>().enabled = false;
        shell.GetComponent<Rigidbody>().velocity += (-shell.transform.forward + shell.transform.right * 0.5f) * 1;
        Destroy(shell, 0.75f);
    }
}
