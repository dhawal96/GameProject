using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{

    private Vector2 velocity;
    public float smoothTimeY;
    public float smoothTimeX;

    private GameObject player;
    private Player controlscript;
    private GameObject SectionEndCollider1;
    private GameObject SectionEndCollider2;
    public GameObject SectionEndCollider;
    public GameObject boss;
    public GameObject defaultRain;
    public GameObject newRain;
    public GameObject bossSpawn;
    private Vector3 bossVelocity = Vector3.zero;

    public bool bounds;
    public bool stayOnPlayer;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    // Use this for initialization
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        controlscript = player.GetComponent<Player>();
        SectionEndCollider1 = GameObject.FindGameObjectWithTag("SectionEndCollider1");
        SectionEndCollider2 = GameObject.FindGameObjectWithTag("SectionEndCollider2");
        minCameraPos = new Vector3(2.63f, 1.51f, -10f);
        maxCameraPos = new Vector3(17.41f, 5.78f, -10f);
        stayOnPlayer = true;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(1);
        boss.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        defaultRain.SetActive(false);
        newRain.SetActive(true);
        stayOnPlayer = true;
        controlscript.canMove = true;


    }

    private void Update() //turn off MoveForward UI
    {
        switch (SectionEndCollider.name)
        {
            case "SectionEndCollider":
                if (player.transform.position.x >= 20.21f)
                {
                    controlscript.GoImage.enabled = false;
                    SectionEndCollider.SetActive(true);
                }
                break;

            case "SectionEndCollider2":
                if (player.transform.position.x >= 30.927f)
                {
                    controlscript.GoImage.enabled = false;
                    SectionEndCollider.SetActive(true);
                }
                break;

            case "SectionEndCollider3":
                if (player.transform.position.x >= 41.184f)
                {
                    controlscript.GoImage.enabled = false;
                    SectionEndCollider.SetActive(true);
                }
                break;

            case "SectionEndCollider4":
                if (player.transform.position.x >= 51.802f)
                {
                    controlscript.GoImage.enabled = false;
                    SectionEndCollider.SetActive(true);
                }
                break;

            case "SectionEndCollider5":
                if (player.transform.position.x >= 62.303f)
                {
                    controlscript.GoImage.enabled = false;
                    SectionEndCollider.SetActive(true);
                }
                break;

            case "SectionEndCollider6":
                if (player.transform.position.x >= 76.038f)
                {
                    controlscript.GoImage.enabled = false;
                    SectionEndCollider.SetActive(true);
                }
                break;

            case "SectionEndCollider7":
                if (player.transform.position.x >= 85.01f)
                {
                    controlscript.GoImage.enabled = false; 
                    SectionEndCollider.SetActive(true);
                }
                break;

            case "SectionEndCollider8":
                if (player.transform.position.x >= 98.929f)
                {
                    controlscript.GoImage.enabled = false;
                    SectionEndCollider.SetActive(true);
                }
                break;

            case "SectionEndCollider9":
                if (player.transform.position.x >= 111.86f)
                {
                    controlscript.GoImage.enabled = false;
                    SectionEndCollider.SetActive(true);
                }
                break;
        }
    }

    void FixedUpdate()
    {
        if (stayOnPlayer)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
            float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

            transform.position = new Vector3(posX, posY, transform.position.z);

            if (bounds)
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
            }
        }

        else //Boss Appearance
        {
            bossSpawn.SetActive(false);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3 (boss.transform.position.x, boss.transform.position.y, -10f), Time.deltaTime * 2f);
            
        }

        if (transform.position.x == boss.transform.position.x && transform.position.y == boss.transform.position.y)
        {
            StartCoroutine(Wait());
            //stayOnPlayer = true;
        }
    }

}
