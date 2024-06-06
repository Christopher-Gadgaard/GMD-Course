# Blog Post 5 - Creating the Main Menu and Multiplayer Game UI with Unity UI Toolkit

## Introduction
In this post, we will explore how to use Unity's UI Toolkit to design and implement the main menu and multiplayer game UI for our Bomberman-inspired game, "BomberStudent." We will cover the basic setup, styling, and integration with game scripts to create a responsive and visually appealing user interface.

## Designing the Main Menu

### Setting Up the Main Menu
To create our main menu, we started by designing the layout using the UI Builder. The main menu includes buttons for Singleplayer, Multiplayer, and High Scores.

![Main Menu UI](https://github.com/Christopher-Gadgaard/GMD-Course/blob/main/Blog%20Posts/Screenshots/MainMenuUI.png)

#### Key Steps:
1. **Creating the UI Document**: We added a `UIDocument` component to a GameObject in our scene, which serves as the root for our UI elements.
2. **Using the UI Builder**: We designed the main menu layout in the UI Builder, adding a `VisualElement` as the main container and nested `Buttons` for each menu option.
3. **Styling with USS**: We created a stylesheet to style the menu, ensuring a consistent and appealing look.

#### Important Code Snippets:
Here’s a look at how we handled button navigation using the new Input System:

```csharp
private void Awake()
{
    controls = new MainMenuControls();
    controls.MainMenu.MoveUp.performed += _ => MoveSelection(-1);
    controls.MainMenu.MoveDown.performed += _ => MoveSelection(1);
    controls.MainMenu.Select.performed += _ => CurrentButtonAction(); 
}

private void SetSelectedButton(Button button)
{
    currentSelectedButton?.RemoveFromClassList("current");
    currentSelectedButton = button;
    currentSelectedButton.AddToClassList("current");
    currentSelectedButton.Focus();
}
```

This snippet shows the setup for button navigation and selection, ensuring smooth interaction within the main menu.

### Adding Functionality
To handle menu navigation and scene loading, we created a `UINavigationController` script. This script manages button selection and triggers scene changes based on the selected button.

```csharp
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
```

## Creating the Multiplayer Game UI

### Setting Up the Multiplayer UI
For the multiplayer game UI, we focused on displaying player stats such as speed, bomb count, radius, and conveyor count. We used progress bars to visually represent these stats.

![Multiplayer Game UI](https://github.com/Christopher-Gadgaard/GMD-Course/blob/main/Blog%20Posts/Screenshots/MultiplayerUI.png)

#### Key Steps:
1. **Designing in UI Builder**: We created a UI layout with containers for each player's stats, using `ProgressBar` elements to display the values.
2. **Styling with USS**: We styled the progress bars and containers to match the game's retro aesthetic.

#### Important Code Snippets:
Here’s how we set up the progress bars for each stat:
```csharp
private void Awake()
{
    root = GetComponent<UIDocument>().rootVisualElement;
    player1SpeedProgressBar = root.Q<ProgressBar>("player1-speed-bar");
    player2SpeedProgressBar = root.Q<ProgressBar>("player2-speed-bar");
    player1BombProgressBar = root.Q<ProgressBar>("player1-bomb-bar");
    player2BombProgressBar = root.Q<ProgressBar>("player2-bomb-bar");
    player1RadiusProgressBar = root.Q<ProgressBar>("player1-radius-bar");
    player2RadiusProgressBar = root.Q<ProgressBar>("player2-radius-bar");
    player1ConveyorProgressBar = root.Q<ProgressBar>("player1-conveyor-bar");
    player2ConveyorProgressBar = root.Q<ProgressBar>("player2-conveyor-bar");
}
```

### Updating the UI
To update the UI in real-time, we created event listeners that respond to changes in player stats. This ensures that the UI always reflects the current game state.

```csharp
private void OnEnable()
{
    MovementController.OnSpeedChanged += UpdatePlayerSpeed;
    BombController.OnBombsChanged += UpdatePlayerBombs;
    BombController.OnRadiusChanged += UpdatePlayerRadius;
    ConveyorController.OnConveyorsChanged += UpdatePlayerConveyors;
}

private void UpdatePlayerSpeed(string playerId, float speed)
{
    var normalizedSpeed = speed / 15;
    if (playerId == "Player1")
    {
        player1SpeedProgressBar.value = normalizedSpeed * 100;
    }
    else if(playerId == "Player2")
    {
        player2SpeedProgressBar.value = normalizedSpeed * 100;
    }
}
```

This code snippet shows how we handle updates to the speed stat, normalizing the value and updating the corresponding progress bar.

### Conclusion
By utilizing Unity's UI Toolkit, we were able to create a responsive and visually appealing UI for both the main menu and multiplayer game. The combination of the UI Builder for layout design and USS for styling allowed us to achieve a consistent look and feel throughout the game. Integrating the new Input System and real-time updates ensured a smooth and interactive user experience.

With these foundations in place, we can continue to build and refine the user interface, enhancing the overall gameplay experience in "BomberStudent."
