> ## View the entire tutorial on [GameDev Resources](https://gamedev-resources.com/create-a-health-bar-that-hovers-over-the-player-with-ui-toolkit/).
# Introduction

It’s very important to provide the player with status updates on how a character is doing. There’s a wide array of scenarios where this is applicable, but two very common ones are a health bar or a status meter. I recently implemented this behavior in my own game as a status bar that increases over time to show when a guard is becoming more and more alert to your actions. This tutorial will go over the same mechanics that I used to create a runtime bar, but apply them to a health bar that hovers over the player and enemies.

![Example](./images/final.gif)

## Learning Outcomes

There are two ways that you can achieve this within your game. This tutorial will only focus on one of the techniques - which is translating a position from world space to screen space. By the end of this tutorial you will be able to:

1. Understand the difference between your two options.
2. Design a health bar UI using the UI Builder tool.
3. Add a runtime UI to your game.
4. Manipulate the UI by converting world point to screen point.
5. Toggle the visibility of a visual element.

## Prerequisites

1. You will need Unity 2020.3 LTS or later to follow along.
2. This tutorial assumes you already have basic knowledge of Unity and intermediate knowledge of C#.
3. Basic understanding of UI Toolkit and UI Builder.

> Tutorials may work with earlier versions. The version referenced is the on I used.

## Resources

1. [“Official” UI Toolkit Runtime documentation](https://docs.google.com/document/d/1C_c5hrqOrkgYjmD3s04vcKfk-aQ8n007Ti7vUR51SeQ/edit)

Additionally, this project uses the following free assets (thanks guys!):

1. [Low Poly Water](https://assetstore.unity.com/packages/tools/particles-effects/lowpoly-water-107563) by Ebru Dogan
2. [Pirate Kit](https://kenney.nl/assets/pirate-kit) by Kenney
