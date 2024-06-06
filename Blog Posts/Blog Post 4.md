### Blog Post 4 - Transitioning to Unity Animator and Enhancing Bomb Mechanics

## Introduction
In this post, we will discuss our journey from using a custom animated sprite renderer and manual bomb handling scripts to leveraging Unity's Animator and Input System for more efficient and scalable development. We will cover the updates we made to the bomb controller, brick, and explosion animations in our Bomberman-inspired game, "BomberStudent."

## From Custom Animated Sprite Renderer to Unity Animator

### Custom Animated Sprite Renderer
Initially, we used a custom script to handle sprite animations, inspired by the zigurous tutorial. While functional, this method was limited in flexibility and scalability. This prompted us to transition to Unity's Animator component for more sophisticated animation management.

### Transition to Unity Animator

Using Unity's Animator, we created complex animation transitions and states through an intuitive graphical interface. Here’s how we set up our player animations using the Animator.

#### Setting Up the Animator

1. **Creating Animation Clips**: We created animation clips for different player states such as idle, moving up, down, left, and right using Unity's Animation window.

2. **Animator Controller**: We created an Animator Controller to manage different animation states and transitions. States for each animation clip (idle, move up, move down, move left, move right) were added, and conditions for transitioning between these states were defined.

3. **Parameters**: Parameters were introduced in the Animator Controller to control state transitions. Specifically, `MoveX` and `MoveY` float parameters determined the direction of movement, while a boolean parameter `Idle` checked if the player was stationary.

4. **Transitions**: Transitions between states were defined using the `MoveX` and `MoveY` parameters. For example, a transition from idle to moving right would occur if `MoveX` was greater than 0.

![Animator Setup](https://github.com/Christopher-Gadgaard/GMD-Course/blob/main/Blog%20Posts/Screenshots/Animator.png)

### Implementing the Animator in Movement Controller

In our `MovementController` script, we integrated the Animator to handle animations based on player input. Here are key snippets from the updated script:

#### Updating Animator Parameters

We updated the animator parameters based on the player's velocity:

```csharp
private void UpdateAnimator()
{
    var velocity = myRigidBody2D.velocity;
    var isMoving = velocity.magnitude > 0.1f;
    animator.SetBool(Idle, !isMoving);
    if (isMoving)
    {
        animator.SetFloat(MoveX, velocity.x);
        animator.SetFloat(MoveY, velocity.y);
    }
    else
    {
        animator.SetFloat(MoveX, 0);
        animator.SetFloat(MoveY, 0);
    }
}
```

#### Handling Movement

The `FixedUpdate` method handles the player's movement and updates the Rigidbody2D's velocity:

```csharp
private void FixedUpdate()
{
    var moveInput = inputHandler.MoveInput;
    var movement = moveInput.normalized * speed;
    myRigidBody2D.velocity = movement;
}
```

### Enhancing Bomb Mechanics
#### Original Bomb Controller

We started with a bomb controller that manually handled bomb placement, explosion timing, and destruction effects:

```csharp
private void Update()
{
    if (bombsRemaining > 0 && Input.GetKeyDown(inputKey))
    {
        StartCoroutine(PlaceBomb());
    }
}
```

#### Updated Bomb Controller Using Unity's Input System
We updated the bomb controller to use Unity's Input System and added more sophisticated explosion and animation handling:

##### Setting Up Input Actions
First, we configured input actions for bomb placement:

```csharp
private InputAction placeBombAction;

private void Awake()
{
    var playerInput = GetComponent<PlayerInput>();
    placeBombAction = playerInput.actions["PlaceBomb"];
    placeBombAction.performed += _ => PlaceBomb();
}
```

#### Bomb Placement and Explosion Handling
We improved the bomb placement and explosion handling to support better animations and interactions:

```csharp
private void PlaceBomb()
{
    if (bombsRemaining > 0)
    {
        StartCoroutine(PlaceBombRoutine());
    }
}

private IEnumerator PlaceBombRoutine()
{
    Vector2 position = transform.position;
    position.x = Mathf.Round(position.x);
    position.y = Mathf.Round(position.y);

    var bomb = Instantiate(bombPrefab, position, Quaternion.identity);
    bombsRemaining--;

    yield return new WaitForSeconds(bombFuseTime);

    position = bomb.transform.position;
    position.x = Mathf.Round(position.x);
    position.y = Mathf.Round(position.y);

    var explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
    explosion.PlayStartAnimation();
    explosion.DestroyAfter(explosionDuration);

    AudioManager.Instance.PlaySound(AudioManager.Instance.bombExplodeClip);

    Explode(position, Vector2.up, explosionRadius);
    Explode(position, Vector2.down, explosionRadius);
    Explode(position, Vector2.left, explosionRadius);
    Explode(position, Vector2.right, explosionRadius);

    Destroy(bomb);
    bombsRemaining++;
}
```

#### Brick and Explosion Animations
Similar updates were made for handling brick destruction and explosion animations, ensuring smooth transitions and interactions within the game.

#### Brick Destruction
We created animations for bricks when they are destroyed and integrated them using the Animator component. This allowed us to provide visual feedback and enhance the gameplay experience.

#### Explosion Effects
We designed explosion animations and implemented them in the game. The explosions now visually represent the blast radius and direction, making the game more engaging.

#### Conclusion
By transitioning to Unity's Animator component and updating our scripts to use Unity's Input System, we significantly improved the visual quality and maintainability of our game. These changes not only enhanced the gameplay experience but also made our development process more efficient and scalable.
