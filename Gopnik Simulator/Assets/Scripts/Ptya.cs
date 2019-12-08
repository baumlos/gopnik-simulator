using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ptya : MonoBehaviour
{
    [SerializeField]
    private float timeLeft = 9f;
    public bool active = false;

    void Start()
    {
        SetVisible(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            SetVisible(true);
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = 9f;
                active = false;
                SetVisible(false);
            }

        }

    }

    void SetVisible(bool vis)
    {
        SpriteRenderer spRend = gameObject.transform.GetComponent<SpriteRenderer>();
        Color col = spRend.color;

        if (vis) col.a = 1f;
        else col.a = 0f;

        spRend.color = col;
    }
}
