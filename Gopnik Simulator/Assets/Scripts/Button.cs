using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
   
    public void Exit()
    {

    }

    public void Continue()
    {
        GameObject.Find("MainMenu").GetComponent<MainMenu>().Continue();
    }

    public void Credits()
    {

    }
    
    public void Options()
    {

    }
}
