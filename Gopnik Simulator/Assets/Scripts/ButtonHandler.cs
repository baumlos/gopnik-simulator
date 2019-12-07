using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void startGame() {
        SceneManager.LoadScene("Overworld");
    }


    public void exitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void toTitleScreen() {
        Debug.Log("TitleScreen");
        SceneManager.LoadScene("TitleScreen");
    }

    public void toCredits() {
        Debug.Log("Credits");
        SceneManager.LoadScene("Credits");
    }
}
