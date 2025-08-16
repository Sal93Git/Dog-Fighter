using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class levelSceneManager : MonoBehaviour
{

    [SerializeField] int planesToDefeat = 3;

    GameObject Player;

    [SerializeField] GameObject winMenu;

    [SerializeField] GameObject loseMenu;

    [SerializeField] GameObject tutorialDisplay1;
    [SerializeField] GameObject tutorialDisplay2;
    [SerializeField] GameObject tutorialDisplay3;

    int planesDefeated = 0;

    public TMP_Text textBox; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0f;
        tutorialDisplay1.SetActive(true);
    }

    void Update()
    {
        textBox.text = "Enemies Defeated : "+planesDefeated.ToString()+" / "+planesToDefeat.ToString();
    }

    public void clearTutorialWin1()
    {
        tutorialDisplay1.SetActive(false);
        tutorialDisplay2.SetActive(true);
    }

    public void clearTutorialWin2()
    {
        tutorialDisplay2.SetActive(false);
        tutorialDisplay3.SetActive(true);
    }

    public void clearTutorialWin3()
    {
        tutorialDisplay3.SetActive(false);
        Time.timeScale = 1f;
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
