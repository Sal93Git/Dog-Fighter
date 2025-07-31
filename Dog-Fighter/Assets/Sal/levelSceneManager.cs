using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class levelSceneManager : MonoBehaviour
{

    [SerializeField] int planesToDefeat = 3;

    GameObject Player;

    [SerializeField] GameObject winMenu;

    [SerializeField] GameObject loseMenu;

    int planesDefeated = 0;

    public TMP_Text textBox; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        textBox.text = "Enemies Defeated : "+planesDefeated.ToString()+" / "+planesToDefeat.ToString();
    }

    public void returnToMainMenu()
    {   
        SceneManager.LoadScene("MainMenu");
    }

    public void retryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void incrementCountDefeatedEnemy()
    {
        planesDefeated++;

        if(planesDefeated >= planesToDefeat)
        {
            activateWinMenu();
        }
    }

    void activateWinMenu()
    {
        winMenu.SetActive(true);
    }

    public void activateLoseMenu()
    {
        loseMenu.SetActive(true);
    }


}
