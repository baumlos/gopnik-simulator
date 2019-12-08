using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadaRace : MonoBehaviour
{
    // public GameObject player;

    public float progress = 0; //how far the player came on the race track
    public float length = 350;
    int lane = 1;       //lane the player is on (0-3)
    public float speed = 5f;
    public float speed_up = 0.05f;

    public GameObject[] lanes;  // waypoint for the lanes
    public GameObject goal;

    public GameObject[] background;  // background

    public GameObject explosion;

    public GameObject[] obs_prefabs;  // obstacle prefabs
    List<GameObject> obstacles; //

    public GameObject winScreen;
    public GameObject loseScreen;
    public AudioSource audio;

    private float obstacle_spawn_cooldown = 0f;  // counter for cooldown
    public float obstacle_spawn_cooldown_duration = 2f;  // minimum time between two obstacles
    public float obstacle_prop = 0.5f;  // probability that two obstacles spawn at once
    public float dual_obs_prop = 0.5f;  // probability that two obstacles spawn at once
    public float lane_change_speed = 0.5f;   // rate at which the player changes the lane (lerp)

    public int screen_width = 30;           //size of the screen (for spawn/despawn of obstacles)

    public Texture health_icon;
    public int health = 3;


    bool over = false;
    // Start is called before the first frame update
    void Start()
    {
        obstacles = new List<GameObject>();
        obstacles.Add(Instantiate(goal, new Vector3(length-5, goal.transform.position.y, goal.transform.position.z), goal.transform.rotation));
    }

    // Update is called once per frame
    void Update()
    {
        if (over) { return; }
        if(health <= 0) {
            over = true;
            lose();
            return;
        }

        if (progress >= length) {
            over = true;
            win();
            return;
        }

        //lane switching
        if (Input.GetKeyDown(KeyCode.W) && lane > 0) {    //lane up
            lane--;
        }

        if (Input.GetKeyDown(KeyCode.S) && lane < 3) {    //lane down
            lane++;
        }

        this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, lanes[lane].transform.position, lane_change_speed);

        //move background
        for(int i = 0; i< background.Length; i++) {
            background[i].GetComponent<Rigidbody>().velocity = new Vector3(-speed, 0, 0);

            if (background[i].transform.position.x <= -32) {
                background[i].transform.Translate(new Vector3(background[i].transform.position.x + 95.5f,0,0));// = new Vector3(32, background[i].transform.position.y, background[i].transform.position.z);
            }
        }

        progress += speed * Time.deltaTime;
        speed += speed_up * Time.deltaTime;

        for (int i = 0; i<obstacles.Count; i++){
            //move
            //obstacles[i].transform.position = new Vector3(obstacles[i].transform.position.x - speed * Time.deltaTime, obstacles[i].transform.position.y, obstacles[i].transform.position.z);
            obstacles[i].GetComponent<Rigidbody>().velocity = new Vector3(-speed,0,0);

            //despawn
            if (obstacles[i].transform.position.x <= -screen_width / 2.0f) {
                GameObject o = obstacles[i];
                obstacles.Remove(o);
                DestroyImmediate(o);
            }
        }

        //obstacle spawn
        if (obstacle_spawn_cooldown <= 0) {
            if(Random.Range(0,1) <= obstacle_prop) {
                obstacle_spawn_cooldown = obstacle_spawn_cooldown_duration;
                int obs_lane = Random.Range(0, 4);
                Vector3 obs_position = new Vector3(screen_width/2, lanes[obs_lane].transform.position.y, lanes[obs_lane].transform.position.z);
                obstacles.Add(Instantiate(obs_prefabs[Random.Range(0, obs_prefabs.Length)], obs_position, Quaternion.identity));

                float r = Random.Range(0.0f, 1.0f);
                if (r <= dual_obs_prop) {
                    obstacle_spawn_cooldown = obstacle_spawn_cooldown_duration;
                    int obs_lane2;
                    do {
                        obs_lane2 = Random.Range(0, 4);
                    } while (obs_lane == obs_lane2);

                    obs_position = new Vector3(screen_width / 2, lanes[obs_lane2].transform.position.y, lanes[obs_lane2].transform.position.z);
                    obstacles.Add(Instantiate(obs_prefabs[Random.Range(0, obs_prefabs.Length)], obs_position, Quaternion.identity));
                }

            }
        } else {
            obstacle_spawn_cooldown -= Time.deltaTime;
        }

    }


    //gui for health
    void OnGUI() {
        for (int i = 0; i < health; i++) {
            GUI.Label(new Rect(10 + 40 * i, 10, 70, 70), health_icon);
        }
    }

    void OnTriggerEnter(Collider collision) {
        health -= 1;
        gameObject.GetComponent<AudioSource>().Play(0);
        obstacles.Remove(collision.gameObject);
        Destroy(collision.gameObject);
    }

    void stopGame() {
        foreach(GameObject o in obstacles) {
            o.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        for(int i = 0; i<2; i++) {
            background[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    void win() {
        Instantiate(winScreen);
        GlobalVariables.addVodka(health * 100);
        stopGame();
        Debug.Log("You win!");
    }

    void lose() {
        audio.volume = 0.1f;
        Instantiate(explosion, this.gameObject.transform);
        Instantiate(loseScreen);
        stopGame();
        Debug.Log("You lost!");
    }
}
