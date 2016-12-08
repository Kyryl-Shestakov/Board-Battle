using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandling : MonoBehaviour
{
	public void StartGame ()
	{
        SceneManager.LoadSceneAsync("BoardScene");
	}

    public void ShowRecords()
    {
        SceneManager.LoadSceneAsync("RecordsScene");
    }

    public void ShowSettings()
    {
        SceneManager.LoadSceneAsync("SettingsScene");
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadSceneAsync("MainMenuScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
