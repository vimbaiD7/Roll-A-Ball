using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CollisionTests
{
    [UnityTest]
    public IEnumerator PlayerCollidesWithCollectible()
    {
        // Arrange - Create player
        GameObject player = new GameObject("Player");
        player.tag = "Player";
        Rigidbody rb = player.AddComponent<Rigidbody>();
        SphereCollider playerCollider = player.AddComponent<SphereCollider>();
        player.transform.position = Vector3.zero;
        
        // Create collectible
        GameObject collectible = new GameObject("Collectible");
        SphereCollider collectibleCollider = collectible.AddComponent<SphereCollider>();
        collectibleCollider.isTrigger = true;
        collectible.transform.position = Vector3.zero; // Same position as player
        
        bool collisionDetected = false;
        
        // Add collision detection
        var collisionDetector = collectible.AddComponent<TestCollisionDetector>();
        collisionDetector.OnTriggerCallback = () => collisionDetected = true;
        
        // Act - Wait for physics to detect collision
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        
        // Assert
        Assert.IsTrue(collisionDetected, "Player should collide with collectible");
        
        // Cleanup
        Object.Destroy(player);
        Object.Destroy(collectible);
    }
    
    [UnityTest]
    public IEnumerator RigidbodyRespondsToForce()
    {
        // Arrange
        GameObject ball = new GameObject("Ball");
        Rigidbody rb = ball.AddComponent<Rigidbody>();
        rb.useGravity = false;
        ball.transform.position = Vector3.zero;
        
        Vector3 startPosition = ball.transform.position;
        
        // Act - Apply force
        rb.AddForce(Vector3.forward * 10f, ForceMode.Impulse);
        
        // Wait for physics
        yield return new WaitForSeconds(0.5f);
        
        // Assert - Ball should have moved
        Assert.Greater(ball.transform.position.z, startPosition.z, 
            "Ball should move forward after force applied");
        
        // Cleanup
        Object.Destroy(ball);
    }
}

// Helper class for collision detection in tests
public class TestCollisionDetector : MonoBehaviour
{
    public System.Action OnTriggerCallback;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTriggerCallback?.Invoke();
        }
    }
}