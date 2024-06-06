# Blog Post 6 - Finalizing the Course: Adding Custom Pickups and Audio

## Introduction
As we wrap up our game development course, we'll look at adding a custom pickup item—the conveyor belt—and incorporating audio to enhance the gameplay experience. This final blog post will cover the implementation details and provide a summary of what we've accomplished throughout the course.

## Adding a Custom Pickup Item: Conveyor Belt

### Implementing the Conveyor Belt
The conveyor belt is a unique pickup item that players can place to influence bomb movement. Let's go through the key steps and code snippets to add this feature.

#### Key Steps:
1. **Creating the Conveyor Controller**: This controller handles the placement and count of conveyor belts for each player.
2. **Initializing Input Actions**: Using Unity's Input System to handle player actions for placing conveyor belts.
3. **Handling Conveyor Placement**: Logic for placing the conveyor belt in the game world and initializing its direction.

#### Important Code Snippets:

```csharp
private void InitializeMoveAction(PlayerInput playerInput)
{
    moveAction = playerInput.actions["Move"];
    moveAction?.Enable();
}
```

```csharp
private void PlaceConveyorBelt()
{
    if (conveyorBeltCount <= 0) return;
    if (moveAction == null) return;

    Vector2 position = transform.position;
    position.x = Mathf.Round(position.x);
    position.y = Mathf.Round(position.y);

    var conveyorBelt = Instantiate(conveyorBeltPrefab, position, Quaternion.identity);
    var conveyorBeltScript = conveyorBelt.GetComponent<ConveyorBelt>();
    if (conveyorBeltScript != null)
    {
        var playerDirection = moveAction.ReadValue<Vector2>();
        conveyorBeltScript.InitializeDirection(playerDirection);
    }

    conveyorBeltCount--;
    OnConveyorsChanged?.Invoke(playerId, conveyorBeltCount);
}
```

### Enhancing Bomb Mechanics with Conveyor Belts
The conveyor belts affect bomb movement by boosting their speed in a specific direction.

#### Important Code Snippets:

```csharp
public void InitializeDirection(Vector2 playerDirection)
{
    direction = playerDirection == Vector2.zero ? Vector2.down : playerDirection.normalized;
    RotateConveyor(direction);
}
```

### Creating Custom Sprites
We created our own sprite for the conveyor belt to visually represent its function in the game. The direction of the conveyor belt is set based on the player's current movement direction.

![Conveyor Belt Sprite](https://github.com/Christopher-Gadgaard/GMD-Course/blob/main/GMD%20-%20BomberStudent/Assets/Game/Sprites/Tiles/Conveyor_0.png)

#### Key Steps:
1. **Designing the Sprite**: Using a pixel art tool, we designed a conveyor belt sprite with clear directional indicators.
2. **Integrating the Sprite**: Imported the sprite into Unity and set it up in the ConveyorBelt script to rotate based on the direction.

#### Important Code Snippets:

```csharp
private void FixedUpdate()
{
    var moveInput = inputHandler.MoveInput;
    var movement = moveInput.normalized * speed;
    myRigidBody2D.velocity = movement;
}
```

## Adding Audio to the Game

### Setting Up the AudioManager
To manage sound effects and background music, we created an `AudioManager` singleton that handles audio playback across the game.

#### Key Steps:
1. **Creating AudioManager**: This script manages different audio clips and their playback.
2. **Playing Background Music**: Ensuring continuous background music during gameplay.
3. **Playing Sound Effects**: Playing sound effects for various game events like pickups and explosions.

#### Important Code Snippets:

```csharp
public void PlaySound(AudioClip clip)
{
    effectsSource.PlayOneShot(clip);
}
```

### Integrating Audio with Game Events
We integrated audio playback with various game events, such as picking up items and placing conveyor belts and added background music.

#### Important Code Snippets:

```csharp
private void OnItemPickup(GameObject player)
{
    // Existing item handling code
    AudioManager.Instance.PlaySound(AudioManager.Instance.pickupItemClip);
    Destroy(gameObject);
}
```

## Conclusion
Throughout this course, we have developed a Bomberman-inspired game from scratch, learning and implementing various game development concepts along the way. While we didn't complete all our initial goals, such as implementing single-player mode and a high score system, we are still proud of what we achieved. We've covered:

- Basic game setup and mechanics
- Creating and managing a game design document
- Developing core gameplay features
- Using Animation to make the game come alive
- Implementing a responsive and appealing user interface
- Adding custom game elements and integrating audio

This journey has not only expanded our knowledge of Unity and game development but also provided us with a solid foundation to build more complex and engaging games in the future.

Thank you for following along, and happy game developing!
