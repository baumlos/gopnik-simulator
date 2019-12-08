using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public GameObject ui_element_press_e;
    [Range(0.0f, 1000.0f)]
    public float vodkaLevel;
    
    public Image vodkameter;

    private void Update() {
        GlobalVariables.vodka_level = vodkaLevel;
        updateVodkaLevel();

    }

    public void activatePressE() {
        ui_element_press_e.SetActive(true);
    }
    public void deactivatePressE() {
        ui_element_press_e.SetActive(false);
    }

    public void tooglePressE() {
        ui_element_press_e.SetActive(!ui_element_press_e.active);
    }

    public void updateVodkaLevel() {
        vodkameter.fillAmount = GlobalVariables.vodka_level/GlobalVariables.max_vodka_level;
    }
}
