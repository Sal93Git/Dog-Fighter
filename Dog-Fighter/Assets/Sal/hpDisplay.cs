using UnityEngine;
using TMPro;

public class hpDisplay : MonoBehaviour
{
    public TMP_Text textBox; 
    public GameObject player;
    float valueToDisplay; 
    
    void Update()
    {
        if (player == null) return;
        
        valueToDisplay = player.GetComponent<PlaneController>().planeHitPoints;
        textBox.text = "HP : "+Mathf.RoundToInt(valueToDisplay).ToString();
    }
}
