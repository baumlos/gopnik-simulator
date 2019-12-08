using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAroundSprite : MonoBehaviour
{
    public Transform player;
    public int minLayer;
    public int maxLayer;
    private SpriteRenderer rend;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //player behind you
        if(player.transform.position.z > transform.position.z)
        {
            rend.sortingOrder = maxLayer;
        }
        else
        {
            rend.sortingOrder = minLayer;
        }
          
    }
}
