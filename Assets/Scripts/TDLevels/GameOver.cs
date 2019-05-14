using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text roundsText;
    public string MenuScene;
    public ScreenFade fade;

    void OnEnable()
    {
        roundsText.text = PlayerStats.roundCount.ToString();
    }

    public void Retry()
    {
        fade.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        fade.FadeTo(MenuScene);
    }
}
