using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public float RotationSpeed = 5f;
    public GameObject Propeller;
    void Update()
    {
        Propeller.transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);   
    }
}
