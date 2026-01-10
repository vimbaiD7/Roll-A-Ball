using UnityEngine;
using TMPro;

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
    
    
    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject levelUpPanel;
    
    [Header("Game Over Elements")]
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI gameOverMessageText;
    public TextMeshProUGUI finalHighScoreText;
    
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
        // pause with ESC key
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
    }
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
    
   
    public void ShowGameOver(int finalScore, int oldHighScore)
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f; 
        
            if (finalScoreText != null)
            {
                finalScoreText.text = "Score: " + finalScore.ToString();
            }
            
            bool isNewHighScore = finalScore > oldHighScore;
        
            if (isNewHighScore)
            {
                // NEW HIGH SCORE!
                Debug.Log("Displaying NEW HIGH SCORE screen");
                if (highScoreText != null)
                {
                    highScoreText.gameObject.SetActive(false);
                }
                if (newHighScoreText != null)
                {
                    newHighScoreText.SetActive(true);
                }
            
                if (gameOverMessageText != null)
                {
                    gameOverMessageText.text = "NEW HIGH SCORE!";
                }
            }
            else
            {
                Debug.Log("Displaying normal game over. Final: " + finalScore + " Best: " + oldHighScore);
            
                //  old best score
                if (highScoreText != null)
                {
                    highScoreText.gameObject.SetActive(true);
                    highScoreText.text = "Best: " + oldHighScore.ToString();
                }
                if (newHighScoreText != null)
                {
                    newHighScoreText.SetActive(false);
                }
            
                if (gameOverMessageText != null)
                {
                    gameOverMessageText.text = "GAME OVER";
                }
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
                // score from GameManager
                int currentScore = GameManager.Instance != null ? GameManager.Instance.score : 0;
                pauseScoreText.text = "Current Score: " + currentScore.ToString();
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