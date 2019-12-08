using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WesternSpy_Movement : MonoBehaviour { 

    [SerializeField]
    private GameObject spawner;
    [SerializeField]
    private GameObject instructions;
    [SerializeField]
    private GameObject win;
    [SerializeField]
    private GameObject loose;
    [SerializeField]
    private GameObject instructionsText;

    private int counter;
    private float distance;
    static private int pos = -1;

    static private bool finished = false;
    static private int timer = 0;

    static bool right = false;
    static bool left = false;
    static bool started = true;
    // Start is called before the first frame update
    void Start()
    {
        counter = spawner.GetComponent<WesternSpy_Spawner>().getCounter();
        distance = spawner.GetComponent<WesternSpy_Spawner>().getDistance();
        if (pos != -1)
        {
            this.gameObject.transform.SetPositionAndRotation(new Vector3(pos * distance, 0, -10), new Quaternion(0, 0, 0, 1));
            if (finished)
            {
                if (pos == spawner.GetComponent<WesternSpy_Spawner>().getSpy())
                {
                    win.GetComponent<Renderer>().enabled = true;
                }
                else
                {
                    loose.GetComponent<Renderer>().enabled = true;
                }
            }
            instructions.GetComponent<Renderer>().enabled = !started;
            instructionsText.GetComponent<GameUI>().deactivatePressE();
            win.GetComponent<GameUI>().deactivatePressE();
            loose.GetComponent<GameUI>().deactivatePressE();

        }
        else
        {
            right = false;
            left = false;
            started = false;
            finished = false;
            timer = 0;
            pos = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished)
        {
            if (started)
            {
                instructionsText.GetComponent<GameUI>().deactivatePressE();
                instructions.GetComponent<Renderer>().enabled = false;
                float target = pos * distance;
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(target, 0, -10), 0.1f);
                if (Mathf.Abs(this.gameObject.transform.position.x - target) <= 0.02f)
                {
                    this.gameObject.transform.SetPositionAndRotation(new Vector3(target, 0, -10), new Quaternion(0, 0, 0, 1));
                    left = false;
                    right = false;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    if (!right && pos < counter - 1)
                    {
                        right = true;
                        pos++;
                        //this.gameObject.transform.SetPositionAndRotation(this.gameObject.transform.position + new Vector3(distance, 0, 0), this.gameObject.transform.rotation);
                    }
                }
                else
                {
                    right = false;
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    if (!left && pos > 0)
                    {
                        left = true;
                        pos--;
                        //this.gameObject.transform.SetPositionAndRotation(this.gameObject.transform.position - new Vector3(distance, 0, 0), this.gameObject.transform.rotation);
                    }
                }
                else
                {
                    left = false;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (pos == spawner.GetComponent<WesternSpy_Spawner>().getSpy())
                    {
                        win = Instantiate(win);
                    } else
                    {
                        loose = Instantiate(loose);
                    }
                    finished = true;
                }
            }
            else
            {
                if (!Input.GetKey(KeyCode.Escape) && Input.anyKeyDown)
                {
                    started = true;
                    left = true;
                    right = true;
                    instructionsText.GetComponent<GameUI>().deactivatePressE();
                    instructions.GetComponent<Renderer>().enabled = false;
                }
            }
        } else
        {
            timer++;
            /*if(!Input.GetKey(KeyCode.Escape) && Input.anyKeyDown && timer > 10)
            {
                if (win.GetComponent<GameUI>(). == true)
                {
                    SceneManager.LoadScene(0);
                }
                else
                {
                    pos = -1;
                    spawner.GetComponent<WesternSpy_Spawner>().resetSpy();
                    SceneManager.LoadScene("WesternSpy");
                }
            }*/
        }
    }
}
