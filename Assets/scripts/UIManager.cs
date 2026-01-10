using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [Header("Pause Elements")]
    public TextMeshProUGUI pauseScoreText;
    
    [Header("HUD Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI levelText;
    public GameObject newHighScoreText;
    
    [Header("Visual Hearts (Optional)")]
    public Image[] heartImages;
    
    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject levelUpPanel;
    
    [Header("Game Over Elements")]
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI gameOverMessageText;
    
    [Header("Level Up Elements")]
    public TextMeshProUGUI levelUpText;
    
    private bool isPaused = false;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
   
        if (pausePanel != null) pausePanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (levelUpPanel != null) levelUpPanel.SetActive(false);
        
        UpdateHighScore(PlayerPrefs.GetInt("HighScore", 0));
    }
    
    void Update()
    {
        // Pause with ESC key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    

    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
    
  
    public void UpdateHighScore(int highScore)
    {
        if (highScoreText != null)
        {
            highScoreText.text = "Best: " + highScore.ToString();
        }
    }
    
   
    public void UpdateLives(int lives)
    {
        if (livesText != null)
        {
            livesText.text = " x " + lives.ToString();
        }
        
    
        if (heartImages != null && heartImages.Length > 0)
        {
            for (int i = 0; i < heartImages.Length; i++)
            {
                if (heartImages[i] != null)
                {
                    heartImages[i].enabled = (i < lives);
                }
            }
        }
    }
    
    // Update level display
    public void UpdateLevel(int level)
    {
        if (levelText != null)
        {
            levelText.text = "Level " + level.ToString();
        }
    }
    

    public void ShowLevelUpPopup(int level)
    {
        if (levelUpPanel != null)
        {
            levelUpPanel.SetActive(true);
            
            if (levelUpText != null)
            {
                levelUpText.text = "LEVEL " + level + "!\nSPEED INCREASED!";
            }
            
     
            Invoke("HideLevelUpPopup", 2f);
        }
    }
    
    void HideLevelUpPopup()
    {
        if (levelUpPanel != null)
        {
            levelUpPanel.SetActive(false);
        }
    }
    
   
    public void ShowGameOver(int finalScore, int highScore)
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f; 
            
            if (finalScoreText != null)
            {
                finalScoreText.text = "Final Score: " + finalScore.ToString();
            }
            
         
            if (gameOverMessageText != null)
            {
                if (finalScore > highScore)
                {
                    gameOverMessageText.text = "NEW HIGH SCORE!";
                }
                else
                {
                    gameOverMessageText.text = "GAME OVER";
                }
            }
            if (newHighScoreText != null)
            {
                newHighScoreText.SetActive(finalScore > highScore);
            }
        }
    }
    
    public void TogglePause()
    {
        isPaused = !isPaused;
    
        if (pausePanel != null)
        {
            pausePanel.SetActive(isPaused);
            
            if (isPaused && pauseScoreText != null)
            {
                
                pauseScoreText.text = "Current Score: " + scoreText.text.Replace("Score: ", "");
            }
        }
    
        Time.timeScale = isPaused ? 0f : 1f;
    }
    
    public void Resume()
    {
        isPaused = false;
        if (pausePanel != null) pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void HideGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }
    
    
    
}