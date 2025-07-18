using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
    public TMP_Text textBox; 
    public GameObject player;
    float valueToDisplay; 
    
    void Update()
    {
        valueToDisplay = player.GetComponent<PlaneController>().getCurrentSpeed()*10;
        textBox.text = Mathf.RoundToInt(valueToDisplay).ToString()+"KM/H";
    }
}
