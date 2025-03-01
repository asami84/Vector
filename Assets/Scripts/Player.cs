using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float force;
    private Rigidbody playerRb;
    private  float VerticalInput;
    private GameObject RotatePoint;
    public bool isPowerBon=false;
    public bool isPowerWall=false;
    public bool isPowerScale=false;
    public float ImpulseForce;
    public GameObject PowerDicor;
    public GameObject Walls;
    // Start is called before the first frame update
    void Start()
    {
     playerRb = GetComponent<Rigidbody>();
     RotatePoint=GameObject.Find("RotatePoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y<-27)
        {
            transform.position=new Vector3(0,1.5f,0);
            playerRb.velocity=Vector3.zero;
            playerRb.angularVelocity=Vector3.zero;
        }
        VerticalInput=Input.GetAxis("Vertical");
        playerRb.AddForce(RotatePoint.transform.forward * force*VerticalInput);
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag=="PowerBon")
        {
            GameObject child=other.gameObject;
            GameObject parent=child.transform.parent.gameObject;
            Destroy(parent);
            isPowerBon=true;
            StartCoroutine("PoverOff");
            PowerDicor.SetActive(isPowerBon);
        }
        else if (other.gameObject.tag=="PowerScale")
        {
            GameObject child=other.gameObject;
            GameObject parent=child.transform.parent.gameObject;
            Destroy(parent);
            transform.localScale=new Vector3(5,5,5);
            PowerDicor.SetActive(true);
            isPowerWall=true;
            StartCoroutine("PowerScale");
        }
        if (other.gameObject.tag=="PowerWall")
        {
            GameObject child=other.gameObject;
            GameObject parent=child.transform.parent.gameObject;
            Destroy(parent);
            PowerDicor.SetActive(true);
            Walls.SetActive(true);
            isPowerScale=true;
            StartCoroutine("PowerWall");

        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag=="Enemy" && isPowerBon==true)
        {
            GameObject EnemyOB=other.gameObject;
            Rigidbody EnemyRB=EnemyOB.GetComponent<Rigidbody>();
            Vector3 EnemyPos=EnemyOB.transform.position;
            Vector3 PlayerPos=transform.position;
            Vector3 Direction=(EnemyPos-PlayerPos).normalized;
            EnemyRB.AddForce(Direction*ImpulseForce,ForceMode.Impulse);
        }
    }
    void Transparency(float Walue)
    {
        Renderer[] WallsRend=Walls.GetComponentsInChildren<Renderer>();
        for (int d=0;d<WallsRend.Length;d++)
        {
            Material WallsMat=WallsRend[d].material;
            Color OldColor=WallsMat.color;
            Color WallsColor=new Color(OldColor.r,OldColor.g,OldColor.b,Walue);
            WallsMat.SetColor("_Color",WallsColor);
        }
    }
    IEnumerator PowerWall()
    {
        for (int b=0;b<3;b++)
        {
            yield return new WaitForSeconds(1);
            for (float a=1;a>0;a-=0.1f)
            {
                Transparency(a);
                yield return new WaitForSeconds(0.1f);
            }
            for (float a=0;a<1;a+=0.1f)
            {
                Transparency(a);
                yield return new WaitForSeconds(0.1f);
            }
        }
        isPowerWall=false;
        PowerDicor.SetActive(false);
        Walls.SetActive(false);
    }
    IEnumerator PowerScale()
    {
        yield return new WaitForSeconds(5);
        isPowerScale=false;
        PowerDicor.SetActive(false);
        transform.localScale=new Vector3(2.5f,2.5f,2.5f);

    }
    IEnumerator PoverOff()
    {
        yield return new WaitForSeconds(5);
        isPowerBon=false;
        PowerDicor.SetActive(isPowerBon);
        print("бонус выключен");
    }
}
