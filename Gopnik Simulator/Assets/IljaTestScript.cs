using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IljaTestScript : MonoBehaviour
{
    public Animator myAnimator;


    // Update is called once per frame
    void Start()
    {
        // if(moving)        
        myAnimator.SetBool("walking", true);
    }
}
