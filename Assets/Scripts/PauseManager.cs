using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseManager : MonoBehaviour
{
    public GameObject [] pauseMenuUI;
    public static bool GameIsPause = false;
    public static int numberScen=4;
   
    public void StartPause()
    {
         if (GameIsPause)
            {
               Restart();
            }
            else
            {
                Pause();
            }
    }

    public void ButtonMenu()
    {
        SceneManager.LoadScene("Menu");
        PlayerPrefs.SetInt("PositionPlayer", 0);
        numberScen =  SceneManager.GetActiveScene().buildIndex;
    }

    public void ButtonExit()
    {
        Application.Quit();
    }

    public void ButtonRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    void Pause()
    {
        for (int i=0; i<3; i++)
        {
            pauseMenuUI[i].SetActive(true);
        };
        Time.timeScale = 0f;
        GameIsPause = true;
    }

    void Restart()
    {
        for (int i = 0; i < 3; i++)
        {
            pauseMenuUI[i].SetActive(false);
        };
        Time.timeScale = 1f;
        GameIsPause = false;
    }
}
