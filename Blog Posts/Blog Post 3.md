# Blog Post 3 - Creating the Bomberman Game

## Introduction
In this post, we will dive into the initial stages of creating our Bomberman-inspired game, "BomberStudent." We'll start by setting up our sprites and creating a tilemap to lay the foundation for our game environment.

## Using the Sprite Editor
Before we can start building our game world, we need to prepare our sprites. We have multiple sprite sheets, each containing different elements like characters, bombs, and tiles. Using the Sprite Editor, we sliced these sheets into individual sprites, making them ready for use in our game.

## Creating the Tilemap
With our sprites ready, we began building our game world using tilemaps. Tilemaps allow us to create large, grid-based environments efficiently.

We added a Tilemap GameObject, which comes with a Grid component and a Tilemap Renderer. For our game, we needed two tilemaps: one for destructible tiles and one for indestructible tiles. We duplicated the Tilemap GameObject to create these separate layers, ordering the destructible tiles on top.

## Drawing the Map
Using the Tile Palette, we started designing our game map. We decided to replicate the map from the original Bomberman game, ensuring a familiar and engaging layout. After drawing the map, we added a Tilemap Collider 2D component to both tilemaps to handle collisions, ensuring players and bombs interact correctly with the environment.

## Creating the Player
Next, we moved on to creating our player character.

1. **Create Player GameObject**: We started with an empty GameObject and added a Rigidbody 2D component. To ensure smooth movement and no unintended physics interactions, we set the drag and gravity scale to 0 and froze the Z rotation.
2. **Add Circle Collider 2D**: We added a Circle Collider 2D to the player GameObject. Using a circle collider helps the player navigate around the sharp corners of the tilemap more smoothly, preventing them from getting stuck.

## Adding Visuals to the Player
To make our player visible in the game, we added a child GameObject to the player.

1. **Add Child GameObject**: We created a child GameObject under the player and added an Animator and a Sprite Renderer to it. For now, we are just using the Sprite Renderer to display a static player sprite.
2. **Offset the Child**: We offset the child GameObject to position the feet of our player in the middle of the collider. This setup allows for better alignment and visual accuracy, ensuring that the player's movements look natural and smooth.

## Creating a Movement Controller
Next, we implemented a movement controller for our player. Unlike the zigurous guide, we opted to use the Unity Input System for more flexibility and scalability.

### Key Changes
To modernize our approach and improve maintainability, we updated our movement controller to use Unity's Input System. Here are some key methods and changes:

#### Original Update Method
The original method used keyboard inputs directly:

```csharp
private void Update()
{
    if (Input.GetKey(inputUp))
    {
        SetDirection(Vector2.up, spriteRenderUp);
    }
    else if (Input.GetKey(inputDown))
    {
        SetDirection(Vector2.down, spriteRenderDown);
    }
    else if (Input.GetKey(inputLeft))
    {
        SetDirection(Vector2.left, spriteRenderLeft);
    }
    else if (Input.GetKey(inputRight))
    {
        SetDirection(Vector2.right, spriteRenderRight);
    }
    else
    {
        SetDirection(Vector2.zero, activeSpriteRender);
    }
}
```

#### Updated Method with Input System
We updated the method to use the new Input System, which allows for more robust input handling:

```csharp
private void FixedUpdate()
{
    var moveInput = inputHandler.MoveInput;
    var movement = moveInput.normalized * speed;
    myRigidBody2D.velocity = movement;
}
```
The FixedUpdate method is responsible for updating the player's movement based on the input received from the PlayerInputHandler.

#### PlayerInputHandler
For this to work we need something to handle the inputs, so we made and inputhandler, lets look at the important methods in it.

```csharp
      private void InitializeMoveAction()
        {
            if (playerInput == null) return;
            moveAction = playerInput.actions["Move"];
            if (moveAction == null) return;
            RegisterInputActions();
        }

        private void RegisterInputActions()
        {
            moveAction.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
            moveAction.canceled += _ => MoveInput = Vector2.zero;
        }
```

The InitializeMoveAction method is responsible for setting up the moveAction from the PlayerInput component. 
The RegisterInputActions method sets up the event handlers for the moveAction to update the MoveInput property based on player input. 

### Setting Up Input Actions
To integrate the new Input System, we need to set up input actions in Unity:

1. **Create Input Actions Asset**: This asset will hold all our input mappings.
2. **Define Actions and Bindings**: Open the Input Actions asset and create a new action map for player controls. Add a `Move` action and set up bindings for keyboard keys (WASD or arrow keys) and gamepad sticks.
3. **Add PlayerInput Component**: On the Player GameObject, add a `PlayerInput` component and assign the Input Actions asset to it. This component will handle input events and route them to the appropriate methods in our script.

### Conclusion
By integrating the Unity Input System and updating our movement controller, we have created a flexible and scalable foundation for player movement in our Bomberman game. This setup allows us to easily support multiple input methods and improve the maintainability of our code. With the basics in place we can now move our player, but without any animation.
