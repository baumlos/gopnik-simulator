using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventTrigger : MonoBehaviour
{
    private bool active = false;
    private bool available = true;

    [SerializeField]
    private string scene;

    // Update is called once per frame
    void Update()
    {
      if (active && Input.GetKey(KeyCode.E))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.GetComponent<OverWorld_Walking>().savePos();
                available = false;
            }
            SceneManager.LoadScene(scene);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player") && available)
        {
            active = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            active = false;
            
        }

    }
}
