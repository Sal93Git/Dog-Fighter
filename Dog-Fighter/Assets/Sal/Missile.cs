using UnityEngine;


public class Missile : MonoBehaviour
{
    [SerializeField] 
    float speed = 60f;

    [SerializeField]  
    float lifeTime = 7f;

    bool fired = false;
    Vector3 customForward; 
  

    void Start()
    {
        customForward = transform.up;
    }

    void Update()
    {
        // if(Input.GetMouseButtonDown(fireButton) && !fired)
        // {
        //     fired=true;
        //     transform.SetParent(null);
        //     Destroy(gameObject, lifeTime);  // Destroy missile after some seconds
        // }
        if(fired)
        {
            // transform.position += transform.forward * speed * Time.deltaTime;
            transform.position += customForward * speed * Time.deltaTime;
        }
        
    }

    public void FireMissile()
    {
        fired=true;
        transform.SetParent(null);

        // Destroy missile after some seconds
        Destroy(gameObject, lifeTime);
    }
}
