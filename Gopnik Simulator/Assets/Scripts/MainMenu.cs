using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    static private bool menu = false;
    static private int lastScene;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu)
            {
                Continue();
            } else {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if(player != null)
                {
                    player.GetComponent<OverWorld_Walking>().savePos();
                }
                menu = true;
                lastScene = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    public void Continue()
    {
        menu = false;
        SceneManager.LoadScene(lastScene);
    }
}
