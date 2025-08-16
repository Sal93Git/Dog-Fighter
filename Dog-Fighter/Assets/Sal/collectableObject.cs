using UnityEngine;
using System.Collections;


public enum dropType
{
    health,
    ammo
}

public class collectableObject : MonoBehaviour
{
    public dropType thisObjectType;
    int hpRecoverAmount = 15;
    int ammoRecoverAmount = 500;
    public float dropSpeed = 2;

    void Start()
    {
        StartCoroutine(lifeSpan());
    }

    void Update()
    {
        transform.position -= transform.up * dropSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            if(thisObjectType==dropType.health)
            {
                other.gameObject.GetComponent<PlaneController>().replenishHp(hpRecoverAmount);
            }
            else if(thisObjectType==dropType.ammo)
            {
                other.gameObject.GetComponent<MachineGun>().replenishAmmo(ammoRecoverAmount);
            }
        }
        Destroy(gameObject);
    }

    IEnumerator lifeSpan()
    {
        yield return new WaitForSeconds(30);
        Destroy(gameObject);
    }
}
