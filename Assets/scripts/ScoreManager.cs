using UnityEngine;

public class ScoreManager
{
    private int score = 0;
    
    public int GetScore()
    {
        return score;
    }
    
    public void AddScore(int points)
    {
        // Don't allow negative scores
        if (points < 0)
        {
            Debug.LogWarning("Cannot add negative score!");
            return;
        }
        
        score += points;
    }
    
    public void ResetScore()
    {
        score = 0;
    }
}