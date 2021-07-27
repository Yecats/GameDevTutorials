> ## View the entire tutorial on [GameDev Resources](https://gamedev-resources.com/create-an-item-management-editor-tool-with-ui-toolkit/).
> 
# Introduction

Custom tooling is incredibly helpful for streamlining the development of any game. In times past I’ve created tools for quick access for managing the state of the game (cheats) and a for debugging the AI. Most recently, I’ve needed to create a custom tool for managing the item database and inventories for characters in my time travel game. A custom screen for managing scriptable object assets can be very handy, especially if you want to require certain actions or add custom validation.

This tutorial will cover the fundamentals of creating a custom editor tool, including what scriptable objects are and how to create, delete and bind to one. You will also learn about ListView and the Object Picker.

![Example](final.gif)

## Learning Outcomes
In this part of the series, you will learn how to:

1. How to open a window via the Unity toolbar.
2. What a scriptable object is and how you can create, delete and bind to it in code.
3. How to create and configure a ListView and ObjectPicker.

## Prerequisites

> Tutorials may work with earlier versions. The version referenced is the on I used.

1. Basic knowledge of UI Toolkit and UI Builder. Check out [Create a runtime inventory UI with UI Toolkit](https://gamedev-resources.com/create-an-in-game-inventory-ui-with-ui-toolkit/) if you need a primer.
2.	You need [Unity 2020.3 (LTS)](https://unity3d.com/get-unity/download) or later to follow along with this tutorial.
3.	This tutorial assumes you already have basic knowledge of Unity and intermediate knowledge of C#.

