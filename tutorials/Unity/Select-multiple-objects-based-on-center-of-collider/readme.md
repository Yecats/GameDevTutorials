# How to select multiple objects based on the center of a collider

> This tutorial was created with Unity version 2019.2.

Learn how to create an RTS/RPG "drag-to-select multiple characters" game mechanic without using raycasts to detect collisions. Instead, we'll use the center point of the collider's bounds to detect if it is within the radius of a panel drawn on the UI via the mouse start/end positions. This approach supports perspective camera setups and a can be altered to use any vector3 point. 

![Demo of Final Result](./images/pt-5-4-final-demo.gif)

## Learning Outcomes

1. Continue to work with the Input System by setting up more action / binding mappings.
2. Understand what a colliders "bounds" is and how you can work with it to get useful information.
3. Learn about the different 'spaces' (Screen, World, Local) within Unity. 
4. Learn how to convert a world point to UI screen space. 

## Prerequisites
This is a continuation of the [Listen for the Input System Action events via CSharp](../Listen-for-Input-System-events-via-CSharp/) tutorial, which is part two of the Input System series. You can skip previous tutorials by cloning the starterProject in this section of the repository.

> Note: This tutorial will not cover previous topics in depth (such as how the Input Manager works). It is recommended that you start from the beginning of the series if you are missing any concepts. 

## Table of Contents
- [Setting up the input bindings](./articles/pt-1-setting-up-the-input-bindings.md)
- [Setting up the scene](./articles/pt-2-setting-up-the-scene.md)
- [Moving the panel based on mouse position](./articles/pt-3-moving-the-panel-based-on-mouse-click.md)
- [Scaling the panel based on mouse position](./articles/pt-4-scaling-the-panel-based-on-mouse-position.md)
- [Setup the scene for selecting objects](./articles/pt-5-setup-the-scene-for-selecting-objects.md)
- [Coding the selection logic](./articlespt-6-coding-the-selection-logic.md)
- [Challenge: Extendthe selection logic](./articles/pt-7-challenge-extending-the-selection-logic.md)

## Previous Tutorials in this Series
1. Part 2: [Listen for the Input System Action events via CSharp](../Listen-for-Input-System-events-via-CSharp/)
2. Part 1: [How to make a configurable camera with the new Input System](../How-to-make-a-configurable-camera-with-the-new-Input-System/)

## Resources
1. Comments, concerns and/or questions can be posted [here](https://github.com/Yecats/GameDevTutorials/issues/6).
2. This project uses the [Low Poly: Free Pack](https://www.assetstore.unity3d.com/en/#!/content/58821) by AxeyWorks.



