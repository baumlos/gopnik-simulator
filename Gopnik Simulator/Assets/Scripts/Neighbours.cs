using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neighbours : MonoBehaviour
{
    public static int count;
    private bool win = false;
    public GameObject[] bubbles;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (count == 9)
        {
            win = true;
        }
    }

    
}
