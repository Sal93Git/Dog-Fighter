using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float rotationSpeed = 15f;
    public float speed = 15f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlaneMovement();
    }

    void PlaneMovement()
    {
        float pitch = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime;
        float roll = Input.GetAxis("Horizontal") * (rotationSpeed*2f) * Time.deltaTime;

        transform.Rotate(pitch, 0, -roll, Space.Self);

        // Move forward along the tilted forward vector
        transform.position += transform.forward * speed * Time.deltaTime;

        // Add upward lift proportional to roll to counter downward drift
        float liftAmount = Mathf.Abs(roll) * 2.5f; // tweak 0.5f as needed
        transform.position += Vector3.up * liftAmount * Time.deltaTime;
    }
 
}
