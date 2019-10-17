# How to make a configurable camera with the new Unity Input System
> This tutorial was created with Unity version 2019.2.

With this tutorial, you’re going to build a configurable camera that handles moving, zooming and rotating. This design works great for games that do not want an attached 3rd person camera, but instead want freedom to move around a scene. Instead of using the old input system, we’ll be hooking it up to the new one and will review key concepts as we go.

Configuration features of the camera are:

1. Camera angle
2. Zoom min/max
3. Zoom default
4. Look offset (where you want the camera to focus on the y axis)
5. Rotation speed

![Demo of Final Result](./images/FinishedExample.gif)

## Learning Outcomes

1. Understand key concepts of the new Input System. 
2. Have a configurable camera that can be customized for your game.

> You can clone this repository to get the starter project and follow along!  

## Table of Contents
- [Installing the Input System](./articles/pt-1-installing-the-input-system.md)
- [Setting up the Input System](./articles/pt-2-setting-up-the-input-system.md)
- [Setting up and moving the camera](./articles/pt-3-setting-up-and-moving-the-camera.md)
- [Hooking up the input system to our code](./articles/pt-4-hooking-it-up-to-code.md)
- [Fixing the camera movement](./articles/pt-5-fixing-the-camera-movement.md)
- [Adding zoom behavior](./articles/pt-6-adding-zoom-behavior.md)
- [Adding rotation behavior](./articles/pt-7-adding-rotation-behavior.md)

## Resources
1. Comments, concerns and/or questions can be posted [here](ISSUELINK).
2.	Input System [documentation](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/index.html) and [GitHub repository](https://github.com/Unity-Technologies/InputSystem).
3. [Introducing the new Input System - Unite Copenhagen Video](https://youtu.be/hw3Gk5PoZ6A)
1. This project uses the [Low Poly: Free Pack](https://www.assetstore.unity3d.com/en/#!/content/58821) by AxeyWorks.

