using UnityEngine;
using System.Collections;

public enum target_option
{
    Player,
    Enemy
}

public class MachineGun : MonoBehaviour
{
    [SerializeField] bool AddBulletSpread = true;
    [SerializeField] Vector3 BullSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] ParticleSystem ShootingSytem;
    [SerializeField] Transform BullSpawnPoint;
    [SerializeField] ParticleSystem ImpactParticleSystem;
    [SerializeField] TrailRenderer BullTrail;
    [SerializeField] float ShootDelay = 0.25f;
    [SerializeField] LayerMask mask;

    [SerializeField] AudioSource gunAudio; //TEMP TO JUST GET SOUND IN


    public target_option opponent;
    public int Damage = 3;
    float lastShootTime;

    void Update()
    {
        // Debug.Log(opponent.ToString());
        if (Input.GetMouseButton(1) && gameObject.CompareTag("Player"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (lastShootTime + ShootDelay < Time.time)
        {
            ShootingSytem.Play();
        if (gunAudio != null) //quick and dirty gun sound
        {
            gunAudio.pitch = Random.Range(0.95f, 1.05f);
            gunAudio.Play();
            
        }
            Vector3 direction = GetDirection();
            RaycastHit hit;

            Vector3 endPoint;

            if (Physics.Raycast(BullSpawnPoint.position, direction, out hit, float.MaxValue, mask))
            {
                //Debug.Log("Hit : "+ hit.collider.gameObject.name); 
                endPoint = hit.point;
                if(hit.collider.CompareTag(opponent.ToString()))
                {
                    Debug.Log("Hit : "+ hit.collider.gameObject.name); 
                    if(opponent == target_option.Enemy)
                    {
                        hit.collider.gameObject.GetComponent<AIPlaneController>().applyDamage(Damage);
                    }
                    
                    if(opponent == target_option.Player)
                    {
                        hit.collider.gameObject.GetComponent<PlaneController>().ApplyDamage(Damage);
                    }
                    
                }
            }
            else
            {
                endPoint = BullSpawnPoint.position + direction * 1000f;

                // Create a fake hit for visual consistency
                hit = new RaycastHit();
                hit.point = endPoint;
                hit.normal = -direction;
            }

            TrailRenderer trail = Instantiate(BullTrail, BullSpawnPoint.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));

            lastShootTime = Time.time;

            // Debug line to visualize direction
            Debug.DrawRay(BullSpawnPoint.position, direction * 1000f, Color.red, 1f);
        }
    }

    Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;

        if (AddBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-BullSpreadVariance.x, BullSpreadVariance.x) *0.15f,
                Random.Range(-BullSpreadVariance.y, BullSpreadVariance.y)*0.15f,
                Random.Range(-BullSpreadVariance.z, BullSpreadVariance.z)*0.15f
            );
            direction.Normalize();
        }

        return direction;
    }

    IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPosition = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }

        trail.transform.position = hit.point;

        if (hit.collider != null)
        {
            Instantiate(ImpactParticleSystem, hit.point, Quaternion.LookRotation(hit.normal));
        }

        Destroy(trail.gameObject, trail.time);
    }
}
