using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game State")] public int score = 0;
    public int lives = 3;
    public int level = 1;

    [Header("Difficulty")] public float currentSpeed = 10f;
    public float speedIncrease = 2f;

    [Header("References")] public PlayerController playerController;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Find player if not assigned
        if (playerController == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerController = player.GetComponent<PlayerController>();
            }
        }

        UpdateUI();
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateUI();
        CheckLevelUp();
    }

    public void LoseLife()
    {
        lives--;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateLives(lives);
        }

        Debug.Log("Lives: " + lives);

        if (lives <= 0)
        {
            GameOver();
        }
    }

    void CheckLevelUp()
    {
        int newLevel = (score / 500) + 1;

        if (newLevel > level)
        {
            level = newLevel;
            LevelUp();

            // Speed boost every 5 levels
            if (level % 5 == 0)
            {
                IncreaseSpeed();
            }
        }
    }

    void LevelUp()
    {
        Debug.Log("Level Up! Now Level: " + level);

        // Show popup
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowLevelUpPopup(level);
        }

        // Increase hazard difficulty
        ItemSpawner spawner = FindObjectOfType<ItemSpawner>();
        if (spawner != null)
        {
            spawner.IncreaseDifficulty();
        }

        UpdateUI();
    }

    void IncreaseSpeed()
    {
        Debug.Log("Speed Boost! Level " + level);

        // Increase speed
        currentSpeed += speedIncrease;

        // Update player speed
        if (playerController != null)
        {
            playerController.moveSpeed = currentSpeed;
        }
        else
        {
            Debug.LogWarning("PlayerController not found! Can't increase speed.");
        }

        Debug.Log("New speed: " + currentSpeed);
    }

    void GameOver()
    {
        Debug.Log("Game Over! Final Score: " + score);

        // high score
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        //game over screen
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowGameOver(score, highScore);
        }
        else
        {
            Time.timeScale = 0f;
            Debug.Log("No UIManager found! Game Over.");
            Invoke("RestartGame", 2f);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    void UpdateUI()
    {
        Debug.Log($"Score: {score}, Lives: {lives}, Level: {level}");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");

        Application.Quit();
    }
}
