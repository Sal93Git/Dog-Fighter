using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public float RotationSpeed = 5f;
    public GameObject Propeller;

    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }
    void Update()
    {
        
        float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = transform.position;
        Propeller.transform.Rotate(Vector3.up, (speed * RotationSpeed) * Time.deltaTime);   
    }
}
