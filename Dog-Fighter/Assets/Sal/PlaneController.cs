using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaneController : MonoBehaviour
{
    [SerializeField]  float rotationSpeed = 15f;
    [SerializeField]  float topFlightSpeed = 15f;
    [SerializeField]  float currentSpeed = 0;
    [SerializeField] int planeHitPoints = 100;

    private float pitch;
    private float roll;

    public List<GameObject> missiles = new List<GameObject>();
    public int maxMissileAmmo = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(BuildUpLaunchSpeed());
        foreach (Transform child in transform)
        {
            if(child.gameObject.tag == "missile")
            {
                missiles.Add(child.gameObject);
            } 
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlaneMovement();
    }

    void PlaneMovement()
    {
        if(currentSpeed > (topFlightSpeed * 0.75f))
        {
            pitch = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime;
            roll = Input.GetAxis("Horizontal") * (rotationSpeed*2f) * Time.deltaTime;

            if(Input.GetMouseButtonDown(0))
            {
                FireMissile();
            }
        }

        // transform.Rotate(pitch, 0, -roll, Space.Self);
        transform.Rotate(pitch, 0, -roll);

        // Move forward along the tilted forward vector
        transform.position += transform.forward * currentSpeed * Time.deltaTime;

       
        // Add upward lift proportional to roll to counter downward drift
        float liftAmount = Mathf.Abs(roll) * 0.5f; 
        transform.position += Vector3.up * liftAmount * Time.deltaTime;
    
    }

    void FireMissile()
    {
        if(missiles.Count > 0)
        {
            missiles[0].GetComponent<Missile>().FireMissile();
            missiles.RemoveAt(0);
        }
        else
        {
            Debug.Log("Out of Missile Ammo");
        }
    }

    public void ApplyDamage(int dmgAmount)
    {
        planeHitPoints -= dmgAmount;

        if(planeHitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator BuildUpLaunchSpeed()
    {
        float accelerationRate = 3f;

        while (currentSpeed < topFlightSpeed)
        {
            currentSpeed += accelerationRate * Time.deltaTime;
            currentSpeed = Mathf.Min(currentSpeed, topFlightSpeed); // Clamp to maxSpeed
            yield return null;
        }
    }

 
}
