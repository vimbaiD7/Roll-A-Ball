
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("Game State")]
    public int score = 0;
    public int lives = 3;
    public int level = 1;

    [Header("Difficulty")] 
    public float currentSpeed = 10f;

    public float speedIncrease = 2f;

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
        UpdateUI();
    }

    public void AddScore( int value)
    {
        score += value;
        UpdateUI();
        CheckLevelUp();
    }

    public void LoseLife()
    {
        lives--;
        UpdateUI();

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
        }
    }

    void LevelUp()
    {
        Debug.Log("Level Up! Now Level: " + level);
        currentSpeed += speedIncrease;
    }

    void GameOver()
    {
        Debug.Log("Game Over! Final Score: " + score);
        Invoke("RestartGame",2f);
    }

    void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    void UpdateUI()
    {
        Debug.Log($"Score:  {score},  Lives: {lives}, Level: {level} ");
    }
}
