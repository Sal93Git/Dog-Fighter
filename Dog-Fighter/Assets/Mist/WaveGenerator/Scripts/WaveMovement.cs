using UnityEngine;


public class WaveMovement : MonoBehaviour
{
    public Vector3 driftDirection = new Vector3(1f, 0f, 1f); // Moves diagonally on XZ
    public float driftSpeed = 1f;

    void Update()
    {
        transform.position += driftDirection.normalized * driftSpeed * Time.deltaTime;
    }
}


