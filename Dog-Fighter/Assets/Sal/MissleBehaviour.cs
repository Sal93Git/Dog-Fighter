using UnityEngine;


public class Missile : MonoBehaviour
{
    public float speed = 60f;
    public float lifeTime = 7f;
    bool fired = false;
    Vector3 customForward; 
    public int fireButton = 0;

    void Start()
    {
        customForward = transform.up;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(fireButton) && !fired)
        {
            fired=true;
            transform.SetParent(null);
            Destroy(gameObject, lifeTime);  // Destroy missile after some seconds
        }
        if(fired)
        {
            // transform.position += transform.forward * speed * Time.deltaTime;
            transform.position += customForward * speed * Time.deltaTime;
        }
        
    }
}
