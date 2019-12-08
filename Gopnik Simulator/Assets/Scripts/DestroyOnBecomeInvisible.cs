using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnBecomeInvisible : MonoBehaviour
{
    public BabushkaPlayer player;
    public bool captured = false;

    private void Start()
    {
        player = FindObjectOfType<BabushkaPlayer>();
    }

    private void Update()
    {
        if(transform.position.y < -5)
        {
            player.DestroyVodka();
            Destroy(this.gameObject);
        }
    }


}
