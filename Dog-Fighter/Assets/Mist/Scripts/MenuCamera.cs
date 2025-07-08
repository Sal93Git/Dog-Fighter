using Unity.VisualScripting;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{

    [SerializeField]
    private Transform player; 

    [SerializeField]
    private Vector3 PosOffset = new Vector3(0, 5, -8);
    [SerializeField]
    private Vector3 RotateOffset = new Vector3(0, 1, 0);
    [SerializeField] 
    private float camSpeed = 5f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //if (player == null) return;

        Vector3 desiredPosition = player.position + player.rotation * PosOffset;
        //print(desiredPosition);
        transform.position =  Vector3.Lerp(transform.position, desiredPosition, camSpeed * Time.deltaTime);

        transform.LookAt(player.position + RotateOffset * 2f);

    }
}
