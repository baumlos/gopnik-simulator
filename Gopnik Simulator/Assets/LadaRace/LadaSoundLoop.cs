using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadaSoundLoop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<AudioSource>().Play(19*44100);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
