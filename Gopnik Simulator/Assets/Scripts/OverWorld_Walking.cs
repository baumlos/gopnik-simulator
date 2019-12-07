using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorld_Walking : MonoBehaviour

{

    [SerializeField]
    private GameObject[] AnimationParts;

    [SerializeField]
    private GameObject camera;

    private int pointer = 0;
    private int timer = 0;
    [SerializeField]
    private int maxTimer;
    [SerializeField]
    private float speedx;
    [SerializeField]
    private float speedz;

    [SerializeField]
    private float boundaryTop;
    [SerializeField]
    private float boundaryBot;
    [SerializeField]
    private float boundaryLeft;
    [SerializeField]
    private float boundaryRight;
    [SerializeField]
    private float boundaryCameraLeft;
    [SerializeField]
    private float boundaryCameraRight;

    //public Quaternion r;

    static private float camerax = 0, cameraz = -3.58f;

    private bool left = false;
    static private float posx = 0, posz = 1.2f;


    // Start is called before the first frame update
    void Start()
    {
        camera.transform.SetPositionAndRotation(new Vector3(camerax, 0, cameraz), new Quaternion(0.5735764f, 0, 0, 0.8191521f));
        //r = camera.transform.rotation;

        /*GameObject[] tmp = new GameObject[AnimationParts.Length+1];
        for(int i=0; i<AnimationParts.Length; i++ )
        {
            tmp[i] = AnimationParts[i];
            AnimationParts[i].GetComponent<Renderer>().enabled = false;
        }
        tmp[AnimationParts.Length] = this.gameObject;
        this.gameObject.GetComponent<Renderer>().enabled = false;
        AnimationParts = tmp;*/
        for(int i=1; i<AnimationParts.Length; i++)
        {
            AnimationParts[i].GetComponent<Renderer>().enabled = false;
        }
        AnimationParts[0].GetComponent<Renderer>().enabled = true;
        GameObject.Find("PressEToEnterTheEvent").SetActive( false);
        GameObject.Find("PressEToEnterTheEventLetter").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKey(KeyCode.A) ||Input.GetKey(KeyCode.LeftArrow)) && gameObject.transform.position.x > boundaryLeft)
        {
            posx -= speedx;
            if(camera.transform.position.x - this.gameObject.transform.position.x >= boundaryCameraLeft)
            {
                camera.transform.SetPositionAndRotation(camera.transform.position + new Vector3(-speedx, 0, 0), camera.transform.rotation);
            }
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && gameObject.transform.position.x < boundaryRight)
        {
            posx += speedx;
            if (this.gameObject.transform.position.x - camera.transform.position.x >= boundaryCameraRight)
            {
                camera.transform.SetPositionAndRotation(camera.transform.position + new Vector3(speedx, 0, 0), camera.transform.rotation);
            }
        }
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && gameObject.transform.position.z < boundaryTop)
        {
            posz += speedz;
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && gameObject.transform.position.z > boundaryBot)
        {
            posz -= speedz;
        }
        this.gameObject.transform.SetPositionAndRotation(new Vector3(posx, -4.682443f, posz), this.gameObject.transform.rotation);

        //Animation
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && left == false)
            {
                this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
                left = true;
            }
            if((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && left == true)
            {
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);
                left = false;
            }
            timer++;
            if (timer % maxTimer == 0)
            {
                AnimationParts[0].GetComponent<Renderer>().enabled = false;
                AnimationParts[pointer++ % (AnimationParts.Length-1)+1].GetComponent<Renderer>().enabled = false;
                AnimationParts[pointer % (AnimationParts.Length-1)+1].GetComponent<Renderer>().enabled = true;
            }
        } else
        {
            AnimationParts[pointer % (AnimationParts.Length-1)+1].GetComponent<Renderer>().enabled = false;
            AnimationParts[0].GetComponent<Renderer>().enabled = true;
        }
    }

    public void savePos()
    {
        camerax = camera.transform.position.x;
        cameraz = camera.transform.position.z;
    }
}
