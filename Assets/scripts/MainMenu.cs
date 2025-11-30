using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
    [Header("Menu")]
    
    public GameObject mainMenuScene;
    public GameObject scores;
    public GameObject settings;
    
    private const string GameSceneName = "GameScene"; 

  
    public void StartGame()
    {
        Debug.Log("Starting Game");
        SceneManager.LoadScene("GameScene");
    }


    public void OpenScores()
    {
        Debug.Log("Opening Scores");
        mainMenuScene.SetActive(false);
        scores.SetActive(true);
    }

    public void OpenSettings()
    {
        Debug.Log("Opening Settings...");
        mainMenuScene.SetActive(false);
        settings.SetActive(true);
    }
    
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        
        Application.Quit();

        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    
    public void ReturnToMain()
    {
        Debug.Log("Returning to Main Menu...");
        scores.SetActive(false);
        settings.SetActive(false);
        mainMenuScene.SetActive(true);
    }
}