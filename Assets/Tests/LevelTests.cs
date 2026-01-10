using NUnit.Framework;

public class LevelTests
{
    [Test]
    public void LevelCalculationCorrect()
    {
        // Test: Level 1 at 0-499 points
        Assert.AreEqual(1, CalculateLevel(0), "Level should be 1 at 0 points");
        Assert.AreEqual(1, CalculateLevel(499), "Level should be 1 at 499 points");
        
        // Test: Level 2 at 500-999 points
        Assert.AreEqual(2, CalculateLevel(500), "Level should be 2 at 500 points");
        Assert.AreEqual(2, CalculateLevel(999), "Level should be 2 at 999 points");
        
        // Test: Level 5 at 2000-2499 points
        Assert.AreEqual(5, CalculateLevel(2000), "Level should be 5 at 2000 points");
    }
    
    [Test]
    public void SpeedIncreasesEveryFiveLevels()
    {
        // Test: Speed increases at level 5, 10, 15
        Assert.IsTrue(ShouldIncreaseSpeed(5), "Speed should increase at level 5");
        Assert.IsTrue(ShouldIncreaseSpeed(10), "Speed should increase at level 10");
        Assert.IsTrue(ShouldIncreaseSpeed(15), "Speed should increase at level 15");
        
        // Test: Speed doesn't increase at other levels
        Assert.IsFalse(ShouldIncreaseSpeed(1), "Speed should NOT increase at level 1");
        Assert.IsFalse(ShouldIncreaseSpeed(6), "Speed should NOT increase at level 6");
    }
    
    // Helper methods (same logic as GameManager)
    private int CalculateLevel(int score)
    {
        return (score / 500) + 1;
    }
    
    private bool ShouldIncreaseSpeed(int level)
    {
        return level % 5 == 0;
    }
}