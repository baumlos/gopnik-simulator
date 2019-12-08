using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeigbNormObj : MonoBehaviour
{
    [SerializeField]
    private int time;
    [SerializeField]
    private AudioSource sound;
    public bool active = false;
    float timeLeft;

    void Update()
    {
        if (active)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                Neighbours.count--;
                Debug.Log(Neighbours.count);
                active = false;
            }
        }
    }

    void OnMouseDown()
    {
        if (!active)
        {
            timeLeft = time;
            sound.Play();
            active = true;
            Neighbours.count++;
            Debug.Log(Neighbours.count);

            Speechbubbles[] bubbles = FindObjectsOfType<Speechbubbles>();
            //Debug.Log("bubbles found: " + bubbles.Length);
            int rand = Random.Range(0, bubbles.Length);
            bubbles[rand].active = true;
        }
    }
}
