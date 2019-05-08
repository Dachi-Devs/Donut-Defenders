using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainButtons;
    public GameObject quitButtons;
    public int newgameScene;
    public int testLevel;

    

    public void NewGame()
    {
        SceneManager.LoadScene(testLevel);
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
