using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    [Header("Score Display")]
    public TextMeshProUGUI firstPlaceText;
    public TextMeshProUGUI secondPlaceText;
    public TextMeshProUGUI thirdPlaceText;
    
    void Start()
    {
        LoadAndDisplayScores();
    }
    
    void OnEnable()
    {
        LoadAndDisplayScores();
    }
    
    void LoadAndDisplayScores()
    {
        int highScore1 = PlayerPrefs.GetInt("HighScore1", 0);
        int highScore2 = PlayerPrefs.GetInt("HighScore2", 0);
        int highScore3 = PlayerPrefs.GetInt("HighScore3", 0);
        
        if (firstPlaceText != null)
        {
            firstPlaceText.text = "1st: " + highScore1.ToString();
        }
        
        if (secondPlaceText != null)
        {
            secondPlaceText.text = "2nd: " + highScore2.ToString();
        }
        
        if (thirdPlaceText != null)
        {
            thirdPlaceText.text = "3rd: " + highScore3.ToString();
        }
    }
    
    public static void SaveScore(int newScore)
    {
        int score1 = PlayerPrefs.GetInt("HighScore1", 0);
        int score2 = PlayerPrefs.GetInt("HighScore2", 0);
        int score3 = PlayerPrefs.GetInt("HighScore3", 0);
        
        // Check if new score is a top 3 score
        if (newScore > score1)
        {
            // New 1st place
            score3 = score2;
            score2 = score1;
            score1 = newScore;
        }
        else if (newScore > score2)
        {
            // New 2nd place
            score3 = score2;
            score2 = newScore;
        }
        else if (newScore > score3)
        {
            // New 3rd place
            score3 = newScore;
        }
        
        PlayerPrefs.SetInt("HighScore1", score1);
        PlayerPrefs.SetInt("HighScore2", score2);
        PlayerPrefs.SetInt("HighScore3", score3);
        PlayerPrefs.Save();
    }
}
