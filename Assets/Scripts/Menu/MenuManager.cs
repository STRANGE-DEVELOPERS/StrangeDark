using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Level0");
        Time.timeScale = 1f;
    }

    public void ButtonExit()
    {
        Application.Quit();
    }
}
