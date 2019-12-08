using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FightSliderScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider slider;
    private float currentValue = 0;
    private UnityEvent onProgressComplete;
    void Awake()
    {
        slider = this.gameObject.GetComponent<Slider>();
        if(onProgressComplete == null) onProgressComplete = new UnityEvent();
        onProgressComplete.AddListener(OnProgressComplete);
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public float getCurrentValue(){
        return currentValue;
    }
    public void setCurrentValue(float value){
        currentValue = value;
        if(currentValue > slider.maxValue) onProgressComplete.Invoke();
        else slider.value = currentValue;
    }
    void OnProgressComplete(){
        setActive(false);
    }
    public void setActive(bool a){
        this.gameObject.SetActive(a);
    }
}
