using UnityEngine;


public class WaveMovement : MonoBehaviour
{
    public Vector3 driftDirection = new Vector3(1f, 0f, 1f); // Moves diagonally on XZ
    public float driftSpeed = 1f;
    public Transform Player;
    public float bobSpeed = 10f;     // Speed of the up/down motion
    public float bobHeight = 5f;    // How far it moves up/down
    private float initialY;
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            Player = playerObj.transform;

        initialY = transform.position.y;
    }

    void Update()
    {
        if (Player != null)
        {
            // Only rotate around Y to face the player, preserving X=90°
            Vector3 targetPos = Player.position;
            targetPos.y = transform.position.y; // Ignore height differences

            // Compute direction to player
            Vector3 direction = (targetPos - transform.position).normalized;

            // Convert direction to rotation facing the player
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Combine 90° X rotation with Y-facing rotation
            Quaternion fixedRotation = Quaternion.Euler(90, lookRotation.eulerAngles.y, 0);

            transform.rotation = fixedRotation;
        }

                // Vertical sine wave bobbing
        float newY = initialY + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        Vector3 pos = transform.position;
        pos.y = newY;
        transform.position = pos;
    }

}


