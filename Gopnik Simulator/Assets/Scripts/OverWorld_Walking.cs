using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorld_Walking : MonoBehaviour

{

    //[SerializeField]
    //private GameObject[] AnimationParts;
    [Header("Camera variables")]
    [SerializeField]
    private GameObject camera;
    Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3f;

    [Header("PLayer Variables")]
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

    public float drunkenRange;

    [SerializeField]
    private Animator torsoAnimator;
    //public Quaternion r;

    static private float camerax = 0, cameray = 2.59f, cameraz = -3.7f;

    private bool left = false;
    static private float posx = 0, posz = 1.2f;

    float direction = 0;



    // Start is called before the first frame update
    void Start()
    {
        camera.transform.SetPositionAndRotation(new Vector3(camerax, cameray, cameraz), camera.transform.rotation);
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
        //for(int i=1; i<AnimationParts.Length; i++)
        //{
        //    AnimationParts[i].GetComponent<Renderer>().enabled = false;
        //}
        //AnimationParts[0].GetComponent<Renderer>().enabled = true;
        //GameObject.Find("PressEToEnterTheEvent").SetActive( false);
        //GameObject.Find("PressEToEnterTheEventLetter").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        direction += Random.Range(-drunkenRange, drunkenRange);
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            torsoAnimator.SetBool("walking", true);
            posz += speedz * Mathf.Sin(direction * Time.deltaTime) * Random.Range(0, GlobalVariables.vodka_level / GlobalVariables.max_vodka_level);
            posx += speedx * Mathf.Sin(direction * Time.deltaTime) * Random.Range(0, GlobalVariables.vodka_level / GlobalVariables.max_vodka_level);
            //if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && left == false)
            //{
            //    this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            //    left = true;
            //}
            //if((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && left == true)
            //{
            //    this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            //    left = false;
            //}
            //timer++;
            //if (timer % maxTimer == 0)
            //{
            //    AnimationParts[0].GetComponent<Renderer>().enabled = false;
            //    AnimationParts[pointer++ % (AnimationParts.Length-1)+1].GetComponent<Renderer>().enabled = false;
            //    AnimationParts[pointer % (AnimationParts.Length-1)+1].GetComponent<Renderer>().enabled = true;
            //}
        }
        else
        {
            torsoAnimator.SetBool("walking", false);
            //AnimationParts[pointer % (AnimationParts.Length-1)+1].GetComponent<Renderer>().enabled = false;
            //AnimationParts[0].GetComponent<Renderer>().enabled = true;
        }
        if((Input.GetKey(KeyCode.A) ||Input.GetKey(KeyCode.LeftArrow)) && gameObject.transform.position.x > boundaryLeft)
        {
            posx -= speedx;
            if(camera.transform.position.x - this.gameObject.transform.position.x >= boundaryCameraLeft)
            {
                //camera.transform.SetPositionAndRotation(camera.transform.position + new Vector3(-speedx, 0, 0), camera.transform.rotation);

                camera.transform.position = Vector3.SmoothDamp(camera.transform.position, camera.transform.position + new Vector3(-speedx, 0, 0), ref velocity, smoothTime * Time.deltaTime);

            }
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && gameObject.transform.position.x < boundaryRight)
        {
            posx += speedx;
            if (this.gameObject.transform.position.x - camera.transform.position.x >= boundaryCameraRight)
            {
                //camera.transform.SetPositionAndRotation(camera.transform.position + new Vector3(speedx, 0, 0), camera.transform.rotation);
                camera.transform.position = Vector3.SmoothDamp(camera.transform.position, camera.transform.position + new Vector3(speedx, 0, 0), ref velocity, smoothTime * Time.deltaTime);

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
        this.gameObject.transform.SetPositionAndRotation(new Vector3(posx, transform.position.y, posz), this.gameObject.transform.rotation);

        //if (gameObject.transform.position.x > boundaryRight)
        //    gameObject.transform.SetPositionAndRotation(new Vector3(boundaryRight, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation);
        //if (gameObject.transform.position.x < boundaryLeft) gameObject.transform.SetPositionAndRotation(new Vector3(boundaryLeft, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation);
        //if (gameObject.transform.position.z > boundaryTop) gameObject.transform.SetPositionAndRotation(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, boundaryTop), gameObject.transform.rotation);
        //if (gameObject.transform.position.z < boundaryBot) gameObject.transform.SetPositionAndRotation(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, boundaryBot), gameObject.transform.rotation);

        //Animation
    }

    public void savePos()
    {
        camerax = camera.transform.position.x;
        cameraz = camera.transform.position.z;
    }
}
