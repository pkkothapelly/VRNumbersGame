# VR NumBlocks

A small **VR number puzzle game** built in **Unity and C#** as part of university coursework.

The player interacts with numbered blocks and combines them to create a target value before the timer runs out. The project was built as a complete playable prototype while learning Unity gameplay scripting and XR interaction.

## Demo

**Demo video:**  
https://drive.google.com/file/d/1OqqKoszUgZky_n9nMxm9k2KsObaHZJNM/view?usp=drive_link

## Gameplay

Each round gives the player a randomly generated target number.

Numbered blocks are spawned around the play area with different starting values. When two blocks collide with enough force, their values are combined into a single block.

For example:

`8 + 7 → 15`

If the newly created value matches the current target, the round is completed successfully.

If the timer reaches zero first, the round is lost and a new target is generated.

## Features

- VR number-based puzzle gameplay
- randomly generated target values
- randomly generated numbered blocks
- physics-based block collisions
- block merging and value calculation
- timed rounds
- win and timeout states
- automatic round restarting
- dynamic number labels using TextMesh Pro
- reusable number-block prefab

## Tech Stack

- Unity 2022.3
- C#
- XR Interaction Toolkit
- OpenXR
- Unity Input System
- TextMesh Pro

## Gameplay Systems

### `NumberBlock`

Stores the numerical value of each block and updates the visible number labels when that value changes.

Block values are currently limited to a range of 0–99.

### `BlockMerge`

Handles the core puzzle mechanic.

When two numbered blocks collide above a minimum impact speed, their values are added together. One block keeps the new value while the other is removed.

The resulting value is then checked against the current target.

### `BlockSpawner`

Creates the initial set of numbered blocks at predefined spawn points.

Each spawned block receives a random starting value, allowing the available puzzle pieces to vary between play sessions.

### `GameManager`

Controls the round state.

It:

- generates the target number
- manages the countdown timer
- checks merged block values against the target
- displays success or timeout feedback
- starts a new round after the previous one ends

### `MouseGrabber`

Provides a simple mouse-based grab, release and throw system for interacting with number blocks during desktop testing.

## Project Context

VR NumBlocks was developed as a **university coursework project** and was one of my earlier practical game-development projects.

The project gave me hands-on experience with:

- building gameplay systems in C#
- working with Unity physics
- managing game state and timed rounds
- creating reusable prefabs
- connecting gameplay logic with UI
- working with Unity's XR tooling

## Status

**Complete coursework prototype**

The current gameplay loop is:

`target generated → interact with numbered blocks → merge values → match target before timer expires → start next round`
