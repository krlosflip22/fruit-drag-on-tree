# 🍎 Fruit Tree – Unity Interactive Prototype

**Fruit Tree** is a Unity prototype that demonstrates playful, tactile interactions using custom animation and motion logic — no rigidbody physics involved. The goal is to create a single, deeply interactive feature centered around a fruit tree that spawns fruit which can be picked, dragged, and replaced dynamically.

## 🎮 Gameplay Overview

- A tree spawns fruit at designated points.
- The player can pick a fruit by clicking or tapping on it.
- The picked fruit can be **dragged** and **dropped** anywhere in the scene.
- Once a fruit is picked, a new one **spawns in its place**.
- All interactions feature **custom animations** (jiggle, scale) to give the fruit a dynamic and lively feel.

## 🧠 Code Organization

The codebase is organized around **modular responsibilities**:

Each script follows **single responsibility principles**, making it easy to expand or modify specific behaviors.

## 🔧 How to Run

1. Open the project in Unity (tested in Unity 2022.3.62f1).
2. Load the scene called `TreeScene`.
3. Play the scene in the Unity Editor to interact with the fruit.

> 🍐 Drag, drop, stretch, and enjoy! The tree will keep fruiting as long as you keep picking.

## 🛠️ Technical Notes

- All motion is handled without rigidbody physics.
- Emphasis on animation principles (e.g., squash and stretch, anticipation).
- Designed to feel "juicy" and reactive with minimal components.
