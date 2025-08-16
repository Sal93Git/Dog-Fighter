using UnityEngine;

public class ocean : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            Debug.Log("Player collided into the ocean");
            other.gameObject.GetComponent<PlaneController>().ApplyDamage(100);
        }
    }
}
