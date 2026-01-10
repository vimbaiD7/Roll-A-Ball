using NUnit.Framework;

public class ScoreManagerTests
{
    [Test]
    public void ScoreStartsAtZero()
    {
        // Arrange
        ScoreManager scoreManager = new ScoreManager();
        
        // Act
        int score = scoreManager.GetScore();
        
        // Assert
        Assert.AreEqual(0, score, "Score should start at 0");
    }
    
    [Test]
    public void AddingScoreIncreasesTotal()
    {
        // Arrange
        ScoreManager scoreManager = new ScoreManager();
        
        // Act
        scoreManager.AddScore(100);
        
        // Assert
        Assert.AreEqual(100, scoreManager.GetScore(), "Score should be 100 after adding 100");
    }
    
    [Test]
    public void AddingMultipleScoresAccumulates()
    {
        // Arrange
        ScoreManager scoreManager = new ScoreManager();
        
        // Act
        scoreManager.AddScore(50);
        scoreManager.AddScore(30);
        scoreManager.AddScore(20);
        
        // Assert
        Assert.AreEqual(100, scoreManager.GetScore(), "Score should be 100 after adding 50+30+20");
    }
    
    [Test]
    public void ResetScoreSetToZero()
    {
        // Arrange
        ScoreManager scoreManager = new ScoreManager();
        scoreManager.AddScore(500);
        
        // Act
        scoreManager.ResetScore();
        
        // Assert
        Assert.AreEqual(0, scoreManager.GetScore(), "Score should be 0 after reset");
    }
    
    [Test]
    public void NegativeScoreNotAllowed()
    {
        // Arrange
        ScoreManager scoreManager = new ScoreManager();
        
        // Act
        scoreManager.AddScore(-50);
        
        // Assert
        Assert.AreEqual(0, scoreManager.GetScore(), "Score should not go negative");
    }
}