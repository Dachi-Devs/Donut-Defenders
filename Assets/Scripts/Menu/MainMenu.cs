using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainButtons;
    public GameObject quitButtons;
    public string newgameScene;
    public string testLevel;
    public ScreenFade fade;
    

    public void NewGame()
    {
        fade.FadeTo(testLevel);
    }

    public void CheckQuit()
    {
        mainButtons.SetActive(false);
        quitButtons.SetActive(true);
    }

    public void Return()
    {
        quitButtons.SetActive(false);
        mainButtons.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("QUIT GAME");
    }
}
