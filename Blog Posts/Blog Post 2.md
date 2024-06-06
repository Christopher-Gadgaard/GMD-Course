# Blog Post 2 - Game Design Document

## Introduction
This blog will serve as the Game Design Document for "BomberStudent," a game I will develop during the GMD course. The game is envisioned as a fun, engaging multiplayer experience that captures the spirit of the classic Bomberman arcade game, with my own twist.

# Executive Summary

## Working Title
**BomberStudent**

## Concept Statement
Navigate mazes, plant strategic bombs, and blast away obstacles and opponents. Discover power-ups to enhance your character and engage in explosive competition, either with or against a friend, in this action-packed retro strategy game.

## Genres
Action, Strategy, Multiplayer, Retro, Arcade

## Target Audience
Geared towards retro/arcade gamers and suitable for ages 8 and up.

# Gameplay

## Player Experience
Players will navigate through dynamic mazes filled with challenges. The game will offer a fun mix of strategic planning and fast-paced action, delivering an exhilarating multiplayer and single player experience.

## Core Loops
The primary gameplay loop involves navigating the maze, placing bombs to clear obstacles, combat opponents, and collecting various power-ups to gain an advantage over the adversary.

## Objectives
### Single-Player
The player has to defeat AI opponents by strategically placing bombs to eliminate them.

### Multiplayer
Players aim to be the last one standing in the maze by strategically placing bombs to eliminate the other player. 

# Mechanics
We will be borrowing some game mechanics from the [zigurous bomberman tutorial](https://github.com/zigurous/unity-bomberman-tutorial), but we will also be updating a lot of the code to new standards. This will display ownership of the project and learning throughout the GMD course.

**Update Examples**
- We will update the code to use the new input system.
- We will try and use the built-in animation feature.
- We will implement new features, such as power-ups, new ground tiles with game mechanics, and AI opponents.
- There has to be a UI, and also a scoring system, with a high score board.
- The game must also be made to work with the Arcade machine at VIA.

## Game Systems
Key mechanics include bomb placement, explosions with varying blast radius, destructible and non-destructible obstacles, and a variety of power-ups to modify gameplay dynamically.

### Power-Ups
**Originals**
- Extra Bombs ![ItemExtraBomb](https://github.com/Christopher-Gadgaard/GMD-Course/assets/80517220/5a3ec69a-4e8d-49e3-af80-27cb54be7275)
- Bomb Radius ![ItemBlastRadius](https://github.com/Christopher-Gadgaard/GMD-Course/assets/80517220/f0182946-0b69-44de-971e-126cdac4744c)
- Player Speed ![ItemSpeedIncrease](https://github.com/Christopher-Gadgaard/GMD-Course/assets/80517220/f9832644-4b30-4553-a9c2-8484d4ffb30d)

**New**
- Conveyor Belt (Placed on the ground and when a bomb is push onto it, it then pushes the bomb in the direction of the belt)![ItemSpeedIncrease](https://github.com/Christopher-Gadgaard/GMD-Course/blob/main/GMD%20-%20BomberStudent/Assets/Game/Sprites/Tiles/Conveyor_0.png)

## Interactivity
Players will have direct control over their characters, with responsive movement and bomb placement mechanics. Each action will have immediate feedback, ensuring a tight and engaging gameplay experience.

# Game Elements

## Visual and Audio Style
The game will feature a retro aesthetic, utilizing pixel art graphics and chiptune music to evoke nostalgia while still feeling fresh and contemporary.

# Assets
We will be borrowing some assets from the [zigurous bomberman tutorial](https://github.com/zigurous/unity-bomberman-tutorial), but we will also be making some new assets for the new features in the game.
- 2D sprite assets for characters, bombs, power-ups, and tiles
- User interface elements for in-game status and menus

## Tiles
**Originals**
- Destructibles ![Brick](https://github.com/Christopher-Gadgaard/GMD-Course/assets/80517220/c64dd84c-5fcf-4989-ab85-32f894317197) 
- Indestructibles ![Block](https://github.com/Christopher-Gadgaard/GMD-Course/assets/80517220/38305d36-9a21-4d97-9e61-84b42c8007ed)
- Ground ![Ground](https://github.com/Christopher-Gadgaard/GMD-Course/assets/80517220/00b0bdac-b33c-4502-a534-da081dbe8faa) ![GroundShadow](https://github.com/Christopher-Gadgaard/GMD-Course/assets/80517220/2e269f1d-9eaa-411b-8ab5-3fa0adb15d2f)

# Project Scope and Milestones

## Platforms, Technology, and Scope
The game will be developed for PC using Unity, focusing on creating a solid multiplayer experience. The scope will be defined by the core gameplay mechanics, ensuring they are polished and fun.

## Project Milestones
- Core Gameplay Prototype with Basic Multiplayer - April 1
- Feature Complete Beta with Enhanced Multiplayer Experience - April 30
- Final Polishing and Submission - May 17
