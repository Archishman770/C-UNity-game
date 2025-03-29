# Arthur's Survival Game

A 2D open-world survival game featuring Arthur battling bosses in a pixel art style.

## Project Structure

```
Assets/
├─ Art/
│  ├─ Sprites/       # Character and environment sprites
├─ Prefabs/         # Preconfigured game objects
├─ Scenes/          # Game scenes
├─ Scripts/         # Game code
│  ├─ Player/       # Player controllers
│  ├─ Enemies/      # Enemy AI
│  ├─ Game/         # Game systems
```

## Core Features

- **Player Controller** (`ArthurController.cs`)
  - WASD movement with acceleration/deceleration
  - Melee combat system
  - Health/stamina management

- **Boss AI** (`BossAI.cs`)
  - Chase behavior
  - Attack patterns
  - Enrage mode at low health

- **Test Scene** (`TestScene.unity`)
  - Basic environment setup
  - Player and boss spawn points
  - Camera follow system

## Development Setup

1. Open project in Unity 2022 LTS
2. Import required packages:
   - 2D Tilemap
   - Cinemachine
   - Input System
3. Test scene: `Assets/Scenes/TestScene.unity`

## Next Steps

- Implement procedural world generation
- Add survival mechanics (hunger, crafting)
- Design boss arenas
- Create pixel art assets
