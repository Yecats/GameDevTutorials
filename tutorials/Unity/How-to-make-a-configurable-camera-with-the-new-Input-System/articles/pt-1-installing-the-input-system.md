# Installing the Input System

Unity has been overhauling the Input System to be more robust and to work better for multiple platforms and device configurations. It can also be easily configured to process input for multiple local players (though we will not be doing that in this tutorial). 

> The new Input System is still actively being developed and is considered in preview.

The Input System can be installed via Package Manager or from the GitHub repository. We’ll do Package Manager:

1.	Go to **Window** > **Package Manager**. 
2.	Enable preview packages by going to **Advanced** > **Show Preview Packages**
3.	In the search dialog, type “**Input System**” to search for the package
4.	Select the **Input System** package and click **Install**

Certain aspects of Unity, such as the Universal Render Pipeline, require the old input system to function. For now, it’s best to make sure that the **Active Input Handling** property in your **Project Settings** is set to **Both**. This means that you can use both input systems within your game, but for the sake of this tutorial we will only use the new one. 

You can confirm this is set correctly by going to:

1.	**Edit** > **Project Settings** > **Player** > **Configuration** 

![Active Input Handling Check](..\images\pt-1-2-activeInputHandling.jpg)

### [Previous (Introduction)](..\readme.md)    |     [Next (Setting up the input system)](.\pt-2-setting-up-the-input-system.md)