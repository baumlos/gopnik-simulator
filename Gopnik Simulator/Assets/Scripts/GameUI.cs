using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public GameObject ui_element_press_e;
    [Range(0.0f, 1.0f)]
    public float vodkaLevel;
    public int vodkaMeterSizeMax = 200;

    public Image vodkameter;

    private void Update() {
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
        vodkameter.fillAmount = vodkaLevel;
    }
}
