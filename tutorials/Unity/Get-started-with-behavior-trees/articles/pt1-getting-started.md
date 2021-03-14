# Getting started

There are many different techniques for creating artificial intelligence in a game. Popular ones are Finite State Machines, Fuzzy Logic and Behavior Trees. Some games use just one technique, while others will mix for a more dynamic feel.

Behavior Trees are a fantastic way to write modular AI that can scale in complexity. The tree is made up of various **nodes** that when put together, define of the behavior that will drive the AI. Think of the nodes as several lego blocks that are of different colors and the tree as the final masterpiece. You will reuse the different lego blocks (nodes) to build different masterpieces (trees).

> **Nodes** can also be referred to as **Tasks** or **Leafs**, depending on who is teaching. These mean the same thing. 

Behavior Trees have two main guiding principles:

1. Behaviors should be broken down to the **smallest meaningful reusable component**.
2. Each behavior should be decoupled - that is to not **depend directly on another behavior**.

This tutorial will walk through creating a simple behavior tree from scratch. At the end, you will not only understand the core concepts behind a behavior tree, but you'll have created reusable nodes that you can transfer into your game.

## Download the project files
This tutorial has a starter scene and three scripts that will be used as a jumping off point. You can download the starter project by:

1. Clone and/or download the [GitHub repository](https://github.com/Yecats/GameDevTutorials).
2. Navigate to the Get-started-with-behavior-trees\projects\starterProject folder in Unity.

You'll see **Materials**, **Prefabs**, **Scenes**, and **Scripts** in **Assets/WUG**. Open the **Demo** scene in **Assets/WUG/Scenes**.

## Install Behavior Tree Visualizer
Unity does not have a way to easily visualize how a tree is being drawn. This is especially useful when first learning and as trees get more complex. I have created an open source tool, [Behavior Tree Visualizer](https://github.com/Yecats/UnityBehaviorTreeVisualizer), which this tutorial will use.

Behavior Tree Visualizer (BTV) will scan the scene for active behavior trees and group them in a drop down for easy toggle. Once selected, a graph will be drawn, and nodes will light up, showing you which part of the tree is currently running. You can also trigger the drawing of a specific graph through code - this gives you the ability to add a button to the inspector window, for example. You can read more about the features [here](https://github.com/Yecats/UnityBehaviorTreeVisualizer).

You can install BTV through Package Manager:

1. Go to **Window** > **Package Manager**.
2. Click on the **+** button and choose **Add Package from git URL**.
3. Enter the following URL: https://github.com/Yecats/UnityBehaviorTreeVisualizer.git?path=/com.wug.behaviortreevisualizer

![](../Images/packageManager.gif)

### [Previous (Introduction)](../readme.md)    |     [Next (Understanding Behavior Trees)](./pt2-understanding-behavior-trees.md)


