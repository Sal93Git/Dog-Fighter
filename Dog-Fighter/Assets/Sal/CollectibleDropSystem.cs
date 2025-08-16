using UnityEngine;
using System.Collections;

public class RandomItemDrop : MonoBehaviour
{
    public GameObject itemPrefabA;
    public GameObject itemPrefabB;

    public float dropRadius = 5f;
    public float yOffset = 2f;
    public float dropInterval = 30f;
    [Range(30f, 180f)] 
    public float forwardAngle = 90f;

    GameObject chosenPrefab;

    public Transform player;
    
    private void Start()
    {
        // Start the drop loop
        StartCoroutine(DropLoop());
    }

    private IEnumerator DropLoop()
    {
        while (true)
        {
            SpawnRandomDrop(player.position,player.forward);
            yield return new WaitForSeconds(dropInterval);
        }
    }

    public void SpawnRandomDrop(Vector3 playerPosition, Vector3 playerForward)
    {
        // Normalize forward (ignore Y so drops stay horizontal)
        Vector3 flatForward = new Vector3(playerForward.x, 0f, playerForward.z).normalized;

        // Pick a random angle offset within the forward arc
        float halfAngle = forwardAngle * 0.5f;
        float angleOffset = Random.Range(-halfAngle, halfAngle);

        // Rotate forward vector by the random angle around Y axis
        Quaternion rotation = Quaternion.Euler(0f, angleOffset, 0f);
        Vector3 direction = rotation * flatForward;

        float distance = Random.Range(1f, dropRadius);
     
        Vector3 spawnPos = playerPosition + direction * distance;
        spawnPos.y += yOffset;

        if (Random.value < 0.5f)
        {
            chosenPrefab = itemPrefabA;
        }
        else
        {
            chosenPrefab = itemPrefabB;
        }

        // Spawn object
        if (chosenPrefab != null)
        {
            Instantiate(chosenPrefab, spawnPos, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("One of the item prefabs is not assigned!");
        }
    }

}
