using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UINavigationController : MonoBehaviour
{
    public UIDocument uiDocument;
    private MainMenuControls controls;
    private VisualElement root;
    private Button singleplayerButton;
    private Button multiplayerButton;
    private Button highScoresButton;
    private Button currentSelectedButton;
    private Button[] buttons;
    private int currentIndex;

    private void Start()
    {
        root = uiDocument.rootVisualElement;
        singleplayerButton = root.Q<Button>("Singleplayer");
        multiplayerButton = root.Q<Button>("Multiplayer");
        highScoresButton = root.Q<Button>("HighScore");

        buttons = new[] { singleplayerButton, multiplayerButton, highScoresButton };

        SetSelectedButton(buttons[currentIndex]);
    }

    private void Awake()
    {
        controls = new MainMenuControls();
        controls.MainMenu.MoveUp.performed += _ => MoveSelection(-1);
        controls.MainMenu.MoveDown.performed += _ => MoveSelection(1);
        controls.MainMenu.Select.performed += _ => CurrentButtonAction(); 
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void SetSelectedButton(Button button)
    {
        currentSelectedButton?.RemoveFromClassList("current");
        currentSelectedButton = button;
        currentSelectedButton.AddToClassList("current");
        currentSelectedButton.Focus();
    }

    private void MoveSelection(int direction)
    {
        currentIndex = (currentIndex + direction + buttons.Length) % buttons.Length;
        SetSelectedButton(buttons[currentIndex]);
    }

    private void CurrentButtonAction()
    {
        if (currentSelectedButton == singleplayerButton)
        { 
            LoadScene("SingleplayerScene");
        }
        else if (currentSelectedButton == multiplayerButton)
        {
            LoadScene("MultiplayerScene");
        }
        else if (currentSelectedButton == highScoresButton)
        {
            LoadScene("HighScoresScene");
        }
    }
    
    private static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

