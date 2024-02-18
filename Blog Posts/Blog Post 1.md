# Blog Post 1 - Roll a Ball 
[Play Demo](https://christopher-gadgaard.github.io/GMD_Roll-a-Ball/)

## Introduction
Welcome to my Game Development Course Blog. In this post, we'll dive into the Roll-a-Ball tutorial and expand it beyond its initial scope.

Let's get the ball rolling!

## Why I Chose the Game Development Course
When I began my education, I was almost certain my path would lead me toward IoT and embedded software. However, as I progressed through the semesters, my interest in interactive media grew. This shift was particularly influenced by the Android course I took in the fourth semester, which sparked a newfound fascination in this field. Following that, I enrolled in an XR course last semester, which further fueled my interest. Unfortunately, the XR course did not cover how to use Unity, which meant that I had to rely heavily on my group mates. Despite this, we managed to create two solid projects that I’m proud of developing with my group. So, for this course, I’m really looking forward to learning more about using Unity, game mechanics, and game development in general.

## The Basics: Roll-a-Ball Tutorial
The Roll-a-Ball tutorial was fun and a good starting point for the course. It provided a solid foundation in game development basics, including movement mechanics, game object interactions, and basic UI elements.

### Setting the Scene
The tutorial begins with setting up a simple scene, much of which I had already learned in the XR course. This involves creating a plane to serve as the ground and adding walls to prevent our ball from rolling off into the abyss. Next, we created the player sphere, scaled it, and moved it to its starting location. We then adjusted the lighting a bit and added some colors to it using basic materials.

### Bringing the Ball to Life
Using Unity's Rigidbody component, we added physics properties to the sphere, allowing it to roll, bounce, and react to gravity, thus simulating real-world physics. Next, we learned how to import Unity’s new Input System package. This allowed us to add the player input component to the sphere, paving the way for scripting. Here, we were introduced to 2D vectors and built-in functions like `GetComponent`, `FixedUpdate`, and `AddForce`. Once we completed this setup, we tested it but quickly realized that the camera was not following the ball. To resolve this, we created a quick camera controller script that enables the camera to follow the ball, making sure to use `LateUpdate` because we want the camera to be the last thing to move.

### Collecting Items
To add an objective to our game, the tutorial guides us through the process of creating collectible items. These are small, rotating cubes scattered across the play area, serving not only as visual elements but also as introductions to triggers and Unity's event system. As the ball collides with these cubes, we explore how to program and detect these events, increment a score, and display the current score to the player by creating a small text UI element.


## Expanding The Scope
To enhance this small game, I decided to add a track for racing the ball around, incorporating obstacles such as walls that the player must navigate around, a jump feature, and some "death walls" that must be avoided. I also introduced the ability for the ball to jump when pressing the space bar and implemented a respawn feature to return players to the start if they fall off the track.

Initially, I focused on creating the walls, which taught me a great deal about moving, scaling, and rotating objects. This process was more challenging than anticipated. In the XR course, we utilized Blender for most scene creation, and I found that Unity offered less control in shaping and positioning objects. This complexity arises partly because the pivot point is centrally located in objects, necessitating calculations from the center. Nonetheless, I discovered workarounds, such as placing objects within a parent object to move the pivot to the corner and utilizing the vertex snap tool, which was incredibly helpful.

Next, I addressed how to make the ball jump. This was accomplished by adding a jump action to the Player Input component and creating an `OnJump` method in the script. However, the initial implementation allowed for infinite jumping, which was unsatisfactory. To rectify this, I needed a method to determine if the ball was in contact with the ground. This required the use of a LayerMask and the sphere's transform to calculate the distance to the ground, thus only allowing jumping when in contact.

```csharp
isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
```

Finally, I created the Death Walls and the respawn feature, which were relatively straightforward to implement. The walls were simply colored red with a material and a trigger added on them with a tag. For the respawn, I halted all movement and used a transform.position to move the players back to the start. It was the same respawn method I used if the player falls off the track, and here I just check if the y position of the sphere ever comes under some value.
