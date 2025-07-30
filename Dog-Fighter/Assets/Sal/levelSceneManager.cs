using UnityEngine;
using UnityEngine.SceneManagement;

public class levelSceneManager : MonoBehaviour
{

    [SerializeField] int planesToDefeat = 3;

    GameObject Player;

    [SerializeField] GameObject winMenu;

    [SerializeField] GameObject loseMenu;

    int planesDefeated = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
