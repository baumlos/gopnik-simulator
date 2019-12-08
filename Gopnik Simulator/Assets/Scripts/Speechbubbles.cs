using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speechbubbles : MonoBehaviour
{
    [SerializeField]
    private float timeLeft = 3f;
    public bool active = false;

    void Start()
    {
        SetVisible(false);
    }

    void Update()
    {
        if (active)
        {
            SetVisible(true);
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = 3f;
                active = false;
                SetVisible(false);
            }

        }
        else SetVisible(false);
            
    }

    void SetVisible(bool vis)
    {
        SpriteRenderer spRend = gameObject.transform.GetComponent<SpriteRenderer>();
        // copy the SpriteRenderer's color property
        Color col = spRend.color;
        //  change col's alpha value (0 = invisible, 1 = fully opaque)
        if (vis) col.a = 1f;
        // 0.5f = half transparent
        else col.a = 0f;              // change the SpriteRenderer's color property to match the copy with the altered alpha value
        spRend.color = col;
    }
}