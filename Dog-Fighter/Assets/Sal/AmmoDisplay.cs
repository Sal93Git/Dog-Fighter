using UnityEngine;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
    public TMP_Text textBox; 
    public GameObject player;
    int valueToDisplay; 
    
    void Update()
    {
        if (player == null) return;
        
        valueToDisplay = player.GetComponent<MachineGun>().ammo;
        textBox.text = "AMMO: "+valueToDisplay.ToString();
    }
}
