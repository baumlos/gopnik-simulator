using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSpriteScript : MonoBehaviour
{
    public FightScript c;
    public int bodypart;
    public bool player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown(){
        if(c.getPlayerDefender()&&player){
            Debug.Log("klick");
            c.setPlayerTarget(bodypart);

        }
        else if(!c.getPlayerDefender()&&!player){
            c.setPlayerTarget(bodypart);
        }
    }

}
