# Gladiator

> A modular Unity gameplay architecture sample built around composable entity behavior, dependency injection, and a priority-driven state machine.

Gladiator demonstrates how to keep gameplay logic independent from `MonoBehaviour` and assemble characters from reusable states and providers configured with `ScriptableObject` assets. Behaviors can be replaced or extended without changing the state-machine core.

## Highlights

- **Composable state machine** — enter, update, and exit contracts with priorities, conflict resolution, and deferred state changes.
- **Data-driven entities** — states and providers are assembled through `ScriptableObject` configurations and explicit context requirements.
- **Dependency injection** — Zenject bindings are combined with a local hybrid resolver for dependencies between entity components.
- **Flexible animation layer** — runtime clip overrides, crossfades, animation events, and completion notifications.

## Project Structure

| Area | Location |
| --- | --- |
| State machine | `Assets/Scripts/Core/Services/States` |
| Entity composition | `Assets/Scripts/Core/Behaviors/Entities` |
| Gameplay states and providers | `Assets/Scripts/Core/Behaviors/States`, `Assets/Scripts/Core/Providers` |
| Animation system | `Assets/Scripts/Core/Behaviors/Animations` |
| Dependency injection | `Assets/Scripts/Core/Services/DI` |
| Edit Mode and Play Mode tests | `Assets/Tests` |

## Getting Started

1. Install **Unity 6000.3.16f1**.
2. Clone this repository and open the project in Unity.
3. Open `Assets/Scenes/Main.unity`.
4. Enter Play Mode.

Unity Package Manager restores the required packages from `Packages/manifest.json`.

## Controls

| Action | Input |
| --- | --- |
| Move | `WASD` or arrow keys |
| Look | Mouse |
| Attack | Left mouse button or `Left Ctrl` |

## Technology

Unity · C# · URP · Zenject · Cinemachine · NUnit · NSubstitute
