using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class MenuController : MonoBehaviour
{

    private bool startPressed = false;
    public UIDocument uiDocument; // Assign this in the Inspector
    private Dictionary<string, Button> MainMenuButtons;
    private Dictionary<string, Button> LevelSelectButtons;
    private VisualElement Prestart;
    private VisualElement blackOverlay;
    private VisualElement LevelsList;
    private VisualElement postStart;

void OnEnable()
{
    var root = uiDocument.rootVisualElement;

    postStart = root.Q<VisualElement>("PostStart");
    Prestart = root.Q<VisualElement>("PreStart");
    postStart.AddToClassList("PostStartHidden"); // start hidden

    blackOverlay = root.Q<VisualElement>("BlackOverlay");
    Debug.Log("Found overlay: " + (blackOverlay != null));

    LevelsList = root.Q<VisualElement>("Levels");

    MainMenuButtons = new Dictionary<string, Button>
    {
        { "LevelSelectButton", root.Q<Button>("LevelSelectBut") },
        { "OptionsButton", root.Q<Button>("OptionsBut") },
        { "QuitButton", root.Q<Button>("QuitBut") }
    };

    LevelSelectButtons = new Dictionary<string, Button>
    {
        { "Level1Button", root.Q<Button>("Level1") },
        { "Level2Button", root.Q<Button>("Level2") },
        { "Level3Button", root.Q<Button>("Level3") },
        { "Level4Button", root.Q<Button>("Level4") },
        { "LevelBackButton", root.Q<Button>("BackButton") }, // optional if you implement a Back button
    };

    foreach (var kvp in MainMenuButtons)
    {
        var thisButton = kvp.Value;
        thisButton.clicked += () => OnButtonClicked(kvp.Key);
    }

    // Hook up level button actions
    LevelSelectButtons["Level1Button"].clicked += () => OnLevelClicked(1);
    LevelSelectButtons["Level2Button"].clicked += () => OnLevelClicked(2);
    LevelSelectButtons["Level3Button"].clicked += () => OnLevelClicked(3);
    LevelSelectButtons["Level4Button"].clicked += () => OnLevelClicked(4);
    LevelSelectButtons["LevelBackButton"].clicked += OnBackToMainMenu;

    blackOverlay.RemoveFromClassList("visible"); // ensure it's gone
    blackOverlay.style.opacity = 0f;
    blackOverlay.style.visibility = Visibility.Hidden;
}

    void FadeOutOverlay()
    {
        // Just remove the opacity part first — this triggers fade-out
        blackOverlay.RemoveFromClassList("visible");

        // Delay hiding the element (visibility) until after fade is done
        blackOverlay.schedule.Execute(() =>
        {
            blackOverlay.style.visibility = Visibility.Hidden;
            blackOverlay.style.display = DisplayStyle.None;
        }).StartingIn(500); // Match the 0.5s fade duration in your USS
    }

    void OnButtonClicked(string clickedName)
    {
        Debug.Log("Clicked: " + clickedName);

        // Get the clicked button
        Button clickedButton = MainMenuButtons[clickedName];


        foreach (var kvp in MainMenuButtons)
        {
            string name = kvp.Key;
            Button button = kvp.Value;

            if (name == clickedName)
                continue; // skip clicked button

            // Apply unique behaviors to other buttons
            switch (name)
            {
                case "LevelSelectButton":
                    button.AddToClassList("SlideOutRight");
                    break;

                case "OptionsButton":
                    button.AddToClassList("SlideOutRight");
                    break;

                case "QuitButton":
                    button.AddToClassList("SlideOutLeft");
                    break;
            }
        }
        // Delay (milliseconds) to let clicked button animate first

        clickedButton.schedule.Execute(() =>
{
    switch (clickedName)
    {
        case "LevelSelectButton":
            clickedButton.AddToClassList("LevelSelected");
            LevelsList.AddToClassList("LevelListActive");
            LevelsList.RemoveFromClassList("LevelListDefault");
            break;

        case "OptionsButton":
            clickedButton.AddToClassList("OptionsSelected");
            break;

        case "QuitButton":
            clickedButton.AddToClassList("ClickedPulse");
            break;
    }
}).StartingIn(300);
    }
        void OnLevelClicked(int levelNumber)
        {
            Debug.Log($"Level {levelNumber} button clicked!");

            // Example: Load scene, play sound, show preview, etc.
            // SceneManager.LoadScene($"Level{levelNumber}");
            // Or trigger a custom animation / confirmation
        }

        void OnBackToMainMenu()
        {
            Debug.Log("Back to main menu clicked!");

            // Hide the level list
            LevelsList.RemoveFromClassList("LevelListActive");
            LevelsList.AddToClassList("LevelListDefault");

            // Reset main buttons
            foreach (var kvp in MainMenuButtons)
            {
                kvp.Value.RemoveFromClassList("SlideOutLeft");
                kvp.Value.RemoveFromClassList("SlideOutRight");
                kvp.Value.RemoveFromClassList("LevelSelected");
                kvp.Value.RemoveFromClassList("OptionsSelected");
                kvp.Value.RemoveFromClassList("ClickedPulse");
            }
        }


            void Update()
            {
                if (!startPressed && Input.anyKeyDown)
                {
                    startPressed = true;
                    blackOverlay.style.opacity = StyleKeyword.Null;
                    blackOverlay.style.visibility = StyleKeyword.Null;
                    blackOverlay.schedule.Execute(() =>
                    {
                        Prestart.AddToClassList("PreStartHidden");
                        blackOverlay.AddToClassList("visible");
                    }).StartingIn(0);

                    blackOverlay.schedule.Execute(() =>
                    {
                        FadeOutOverlay();
                        postStart.RemoveFromClassList("PostStartHidden");
                        
                    }).StartingIn(1000); // Delay before fade out starts (1s)
                    print("Classes: " + string.Join(", ", blackOverlay.GetClasses()));
                    print("button pressed");
                }
            }
        }
