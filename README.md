# Zoo World

A 3D Unity game demonstrating a food chain simulation in a zoo environment with various animal types and their interactions.

## 🎮 About the Project

"Zoo World" is an ecosystem simulation where different types of animals interact according to food chain laws. The project is implemented using modern architectural patterns and frameworks.

## ✨ Features

- **Dynamic Animal Spawning** - new animals appear every 1-2 seconds
- **Realistic Food Chain** - predators eat prey, predators fight each other
- **Physical Interactions** - animals collide and bounce according to physics laws
- **Smart Behavior** - animals don't leave screen boundaries
- **Statistics System** - tracking killed animal counts
- **Visual Effects** - "Tasty!" text appears when animals are eaten

## 🐾 Animal Types

### Frog (Prey)
- Moves by jumping at fixed intervals
- Dies when colliding with predators
- Bounces off other prey animals

### Snake (Predator)
- Moves linearly with constant speed
- Eats prey animals
- Fights other predators (one survives randomly)
- Rotates "head" towards movement direction

## 🛠 Technical Features

### Architecture
- **Zenject** - Dependency Injection container
- **UniRx** - Reactive Extensions for Unity
- **Signal Bus** - communication between systems
- **MVVM** - for UI layer
- **Factory Pattern** - animal creation

### Core Systems
- `AnimalSpawner` - animal spawning system
- `WorldBoundsService` - world boundaries management
- `GameStatsHandler` - statistics tracking
- `TastyTextController` - visual effects management

## 📋 Requirements

- **Unity 2022.3.24f1**
- **External Dependencies:**
  - Zenject
  - UniRx

## 🚀 Installation and Setup

1. Clone the repository:
```bash
git clone https://github.com/UnitySt9/Zoo-World.git
```

2. Open the project in Unity 2022.3.24f1

3. Ensure required packages are installed:
   - Zenject
   - UniRx

4. Open the scene from `Scenes/` folder

5. Run the game

## ⚙️ Configuration

Main game parameters can be configured through config files:

- `GameConfig` - spawning and animal behavior settings
- `AnimalRegistry` - animal prefab registration

### Example GameConfig settings:
```csharp
// Spawning intervals
MinSpawnInterval = 1f
MaxSpawnInterval = 2f

// Frog settings
FrogJumpForce = 5f
FrogJumpInterval = 2f
FrogJumpHeight = 2f

// Snake settings
SnakeMoveSpeed = 3f
SnakeDirectionChangeInterval = 3f
SnakeRotationSpeed = 5f
```

## 🎯 Gameplay

- Animals spawn automatically every 1-2 seconds
- Observe interactions between animals
- Monitor statistics in the top-right corner
- Predators show "Tasty!" text when eating other animals

## 📁 Project Structure

```
Assets/
├── _Project/
│   ├── Materials/         # Game materials
│   ├── Prefabs/
│   │   ├── Animals/       # Animal prefabs (Frog, Snake)
│   │   └── UI/            # UI prefabs and elements
│   ├── Scenes/            # Game scenes
│   └── Scripts/
│       ├── Animals/
│       │   ├── Base/      # Animal base classes and interfaces
│       │   ├── Prey/      # Prey classes (Frog)
│       │   ├── Predators/ # Predator classes (Snake)
│       │   └── Factories/ # Animal factories and spawners
│       ├── Core/
│       │   ├── Configs/   # Configuration files (GameConfig, AnimalRegistry)
│       │   ├── Signals/   # Communication signals
│       │   └── Installers/# Zenject dependency injection installers
│       ├── Gameplay/
│       │   └── WorldBounds/# World boundaries service
│       └── UI/
│           ├── Models/    # UI data models
│           ├── ViewModels/# UI view models (MVVM pattern)
│           └── Views/     # UI view components
└── Plugins/               # Third-party plugins and dependencies
```

## 🔧 Extensibility

The project can be easily extended by adding new animal types:

1. Add new `AnimalType` to enum
2. Create animal class inheriting from `AnimalBase`
3. Register prefab in `AnimalRegistry`
4. Configure parameters in `GameConfig`

## 👥 Development

The project is developed with focus on:
- Clean architecture
- Easy maintenance and extensibility
- Modern Unity practices
- Reactive programming

## 📄 License

[Zaur Muchtaroglu]

---

*For questions and suggestions, please create an issue in the repository.*
