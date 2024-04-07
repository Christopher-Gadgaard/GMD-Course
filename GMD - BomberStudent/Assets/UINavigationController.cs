using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

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
    void Start()
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
        if (currentSelectedButton != null)
        {
            // Remove 'selected' class from the previously selected button
            currentSelectedButton.RemoveFromClassList("current");
        }

        // Set the new button as the selected one and update the class list
        currentSelectedButton = button;
        currentSelectedButton.AddToClassList("current");

        // You can also set the focus to the current button if needed
        // This will ensure that if any key is pressed, the currently selected button is activated
        currentSelectedButton.Focus();
    }

    void MoveSelection(int direction)
    {
        // Calculate the new index using modular arithmetic to wrap around the button array
        currentIndex = (currentIndex + direction + buttons.Length) % buttons.Length;

        // Set the new selected button using the new index
        SetSelectedButton(buttons[currentIndex]);
    }
}
