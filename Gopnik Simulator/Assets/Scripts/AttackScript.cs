using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    Vector3 t, s;
    float speed = 20f;
    float timer = 0;
  

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isActiveAndEnabled){
            timer += Time.deltaTime;
            if(timer>1) reset();
            float step = Time.deltaTime * speed;
            transform.position = Vector3.MoveTowards(transform.position,t,step);
        }
    }
    void reset(){
        this.gameObject.SetActive(false);
        timer = 0;
        transform.position = s;
    }
    public void setS(Vector3 s){
        this.s = s;
    }
    public void setT(Vector3 t){
        this.t = t;
    }
}
