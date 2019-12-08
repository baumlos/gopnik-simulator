using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighbTV : MonoBehaviour
{
    [SerializeField]
    private AudioSource sound;
    public GameObject[] objects = new GameObject[9];

    void OnMouseDown()
    {
        Neighbours.count = 0;
        sound.Play();

        Speechbubbles[] bubbles = FindObjectsOfType<Speechbubbles>();
        for (int i = 0; i < bubbles.Length; i++)
        {
            bubbles[i].active = false;
        }

        for (int i = 0; i < 9; i++)
        {
            objects[i].GetComponent<NeigbNormObj>().active = false;
        }
        Debug.Log(Neighbours.count);

        Ptya[] putin = FindObjectsOfType<Ptya>();
        for (int i = 0; i < putin.Length; i++)
        {
            putin[i].active = true;
        }
    }
}
