using UnityEngine;


public class CameraController : MonoBehaviour
{
    [SerializeField] 
    private Transform player; 

    [SerializeField] 
    private Vector3 offset = new Vector3(0, 5, -8);

    [SerializeField] 
    private float camSpeed = 5f;

    void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Calculate rotated offset based on player rotation
        Vector3 desiredPosition = player.position + player.rotation * offset;

        // Smoothly move to the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, camSpeed * Time.deltaTime);

        // keep focus on the player
        transform.LookAt(player.position + Vector3.up * 2f);
    }
}
