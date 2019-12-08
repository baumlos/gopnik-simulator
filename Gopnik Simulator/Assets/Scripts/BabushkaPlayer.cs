using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BabushkaPlayer : MonoBehaviour
{
    enum State { playing, waiting}
    State currentState = State.playing;


    public float speed;
    public int foodCaptured = 0;
    public LayerMask foodLayer;
    public ParticleSystem catchedSomethingEffect;

    //public Transform[] foodCorners;
    public Image[] vodkaBottles;
    int foodIndex = 0;
    float xInput;

    public int live = 3;
    public int foodCapturedToWin;

    [Header("Text to display")]
    public GameObject winningScreen;
    public GameObject losingScreen;

    


    // Start is called before the first frame update
    void Start()
    {
        live = vodkaBottles.Length;
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");

        if(xInput != 0)
        {
            float amtToMove = Time.deltaTime * speed;
            transform.Translate(Vector3.right * xInput * amtToMove, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if(other.gameObject.layer == LayerMask.NameToLayer("Food"))
        {
            Debug.Log("Triggered");
            foodCaptured++;
            other.GetComponent<DestroyOnBecomeInvisible>().captured = true;
            Destroy(other.gameObject);
            //other.gameObject.transform.position = foodCorners[foodIndex].position;
            //foodIndex++;
            catchedSomethingEffect.Play();

            if(foodCaptured > foodCapturedToWin)
            {
                GlobalVariables.addVodka(live * 100);
                Time.timeScale = 0;
                Instantiate(winningScreen);
                //Debug.Log("You won");
                //StartCoroutine(ChangeToOverworld());
            }
        }
    }

    public void DestroyVodka()
    {
        live--;

        if(live < 0)
        {
            Time.timeScale = 0;
            Instantiate(losingScreen);
            //StartCoroutine(ChangeToOverworld());
            //Debug.Log("You lost");
        }
        else
        {
            vodkaBottles[live].enabled = false;
        }
    }


    IEnumerator ChangeToOverworld()
    {
        //maybe add some variables that shall be kept in here
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        Debug.Log("Change to overworld here");
    }

}
