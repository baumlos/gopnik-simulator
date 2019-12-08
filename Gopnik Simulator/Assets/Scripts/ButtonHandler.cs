﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void startGame() {
        Debug.Log("To Overworld");
        Time.timeScale = 1;
        SceneManager.LoadScene("OverworldFinal");
    }


    public void exitGame() {
        Time.timeScale = 1;
        Debug.Log("Quit");
        Application.Quit();
    }

    public void toTitleScreen() {
        Time.timeScale = 1;
        Debug.Log("TitleScreen");
        SceneManager.LoadScene("TitleScreen");
    }

    public void toCredits() {
        Time.timeScale = 1;
        Debug.Log("Credits");
        SceneManager.LoadScene("Credits");
    }

    public void loadScene(string s) {
        Time.timeScale = 1;
        SceneManager.LoadScene(s);
    }
}
