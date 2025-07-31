using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the main menu UI using UI Toolkit.
/// Handles button logic, visual transitions, and input detection.
/// </summary>
public class MenuController : MonoBehaviour
{
    private bool startPressed = false;

    public UIDocument uiDocument; // Assign this in the inspector

    private Dictionary<string, Button> MainMenuButtons;
    private Dictionary<string, Button> LevelSelectButtons;

    private VisualElement Prestart;
    private VisualElement blackOverlay;
    private VisualElement LevelsList;
    private VisualElement postStart;
    private VisualElement OptionsList;
    void OnEnable()
    {
        // Get root element from UI Document
        var root = uiDocument.rootVisualElement;

        // Query important UI elements
        postStart = root.Q<VisualElement>("PostStart");
        Prestart = root.Q<VisualElement>("PreStart");
        blackOverlay = root.Q<VisualElement>("BlackOverlay");
        LevelsList = root.Q<VisualElement>("Levels");
        OptionsList = root.Q<VisualElement>("Options");
        Debug.Log("Found overlay: " + (blackOverlay != null));

        // Start screen hidden at first
        postStart.AddToClassList("PostStartHidden");

        // Create button dictionaries for easier access
        MainMenuButtons = new Dictionary<string, Button>
        {
            { "LevelSelectButton", root.Q<Button>("LevelSelectBut") },
            { "OptionsButton", root.Q<Button>("OptionsBut") },
            { "QuitButton", root.Q<Button>("QuitBut") },
            { "FeedbackButton", root.Q<Button>("FeedbackBut") }
        };

        LevelSelectButtons = new Dictionary<string, Button>
        {
            { "Level1Button", root.Q<Button>("Level1") },
            { "Level2Button", root.Q<Button>("Level2") },
            { "Level3Button", root.Q<Button>("Level3") },
            { "Level4Button", root.Q<Button>("Level4") },
            { "LevelBackButton", root.Q<Button>("BackButton") },
        };

        // Attach click events for main menu buttons
        foreach (var kvp in MainMenuButtons)
        {
            var thisButton = kvp.Value;
            thisButton.clicked += () => OnButtonClicked(kvp.Key);
        }

        // Attach click events for level buttons
        LevelSelectButtons["Level1Button"].clicked += () => OnLevelClicked(1);
        LevelSelectButtons["Level2Button"].clicked += () => OnLevelClicked(2);
        LevelSelectButtons["Level3Button"].clicked += () => OnLevelClicked(3);
        LevelSelectButtons["Level4Button"].clicked += () => OnLevelClicked(4);
        LevelSelectButtons["LevelBackButton"].clicked += OnBackToMainMenu;

        // Ensure overlay is hidden initially
        blackOverlay.RemoveFromClassList("visible");
        blackOverlay.style.opacity = 0f;
        blackOverlay.style.visibility = Visibility.Hidden;

        //disable levels 2-4
        LevelSelectButtons["Level2Button"].AddToClassList("disabled");
        LevelSelectButtons["Level3Button"].AddToClassList("disabled");
        LevelSelectButtons["Level4Button"].AddToClassList("disabled");
    }

    /// <summary>
    /// Fades out the black overlay and hides it after the animation.
    /// </summary>
    void FadeOutOverlay()
    {
        blackOverlay.RemoveFromClassList("visible");

        blackOverlay.schedule.Execute(() =>
        {
            blackOverlay.style.visibility = Visibility.Hidden;
            blackOverlay.style.display = DisplayStyle.None;
        }).StartingIn(500); // Matches USS fade duration
    }

    /// <summary>
    /// Handles button clicks and animates UI transitions accordingly.
    /// </summary>
    /// <param name="clickedName">Name of the button clicked</param>
    void OnButtonClicked(string clickedName)
    {
        Debug.Log("Clicked: " + clickedName);
        Button clickedButton = MainMenuButtons[clickedName];

        // Animate other buttons out
        foreach (var kvp in MainMenuButtons)
        {
            if (kvp.Key == clickedName) continue;

            switch (kvp.Key)
            {
                case "LevelSelectButton":
                case "OptionsButton":
                    kvp.Value.AddToClassList("SlideOutRight");
                    break;

                case "FeedbackButton":
                    kvp.Value.AddToClassList("SlideOutLeft");
                    break;
                case "QuitButton":
                    kvp.Value.AddToClassList("SlideOutRight");
                    break;
            }
        }

        // Delay animation and actions for the clicked button
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
                    OptionsList.AddToClassList("LevelListActive");
                    OptionsList.RemoveFromClassList("LevelListDefault");
                    break;

                case "FeedbackButton":
                    clickedButton.AddToClassList("ClickedPulse");
                    Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSd8MKWOMuA_2iIXD7f5hb-E1bcO0lywVnXk3VztCRtpMRNy8g/viewform?usp=header");
                    break;
                case "QuitButton":
                    Application.Quit();
                    break;
            }
        }).StartingIn(300); // Allow other buttons to animate out first
    }

    /// <summary>
    /// Handles level button clicks.
    /// </summary>
    void OnLevelClicked(int levelNumber)
    {
        Debug.Log($"Level {levelNumber} button clicked!");
        SceneManager.LoadScene($"Level{levelNumber}");
    }

    /// <summary>
    /// Returns to the main menu from the level select screen.
    /// </summary>
    void OnBackToMainMenu()
    {
        Debug.Log("Back to main menu clicked!");

        // Hide level list UI
        LevelsList.RemoveFromClassList("LevelListActive");
        LevelsList.AddToClassList("LevelListDefault");

        // Reset main menu button states
        foreach (var kvp in MainMenuButtons)
        {
            kvp.Value.RemoveFromClassList("SlideOutLeft");
            kvp.Value.RemoveFromClassList("SlideOutRight");
            kvp.Value.RemoveFromClassList("LevelSelected");
            kvp.Value.RemoveFromClassList("OptionsSelected");
            kvp.Value.RemoveFromClassList("ClickedPulse");
        }
    }

    /// <summary>
    /// Detects first user input to trigger UI transition.
    /// </summary>
    void Update()
    {
        if (!startPressed && Input.anyKeyDown)
        {
            startPressed = true;

            // Clear override styles on overlay
            blackOverlay.style.opacity = StyleKeyword.Null;
            blackOverlay.style.visibility = StyleKeyword.Null;

            // Transition start screen out and overlay in
            blackOverlay.schedule.Execute(() =>
            {
                Prestart.AddToClassList("PreStartHidden");
                blackOverlay.AddToClassList("visible");
            }).StartingIn(0);

            // Fade out and reveal main menu
            blackOverlay.schedule.Execute(() =>
            {
                FadeOutOverlay();
                postStart.RemoveFromClassList("PostStartHidden");
            }).StartingIn(1000); // Delay for dramatic effect

            print("Classes: " + string.Join(", ", blackOverlay.GetClasses()));
            print("button pressed");
        }
    }
}
