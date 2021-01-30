# Getting Started
 This tutorial relies on scripts and models that are included in the **starter project**. You can download the **starter project** by:

1. Clone and/or download the [GitHub repository](https://github.com/Yecats/GameDevTutorials). 
2. Navigate to the **Implementing-touch-with-the-new-input-system\projects\starterProject** folder in Unity. 

You will see **Materials**, **Models**, **Prefabs**, **Scenes**, **Scripts** and a **Sprites** folder in **Assets/WUG**. Open the **Demo** scene in **Assets/WUG/Scenes**. 

# Behavior Tree versus State Machine
Behavior Trees and State Machines are both useful tools in the AI world. They can be used separately or together to make interesting and unique AI behaviors. While this tutorial will focus on behavior trees, its useful to have an understanding as to the types of scenarios that you may want to use each technique for.

State machines are great if the AI can operate with fairly shallow logic and a small set of conditions. For example, you might have a siege tower that is either in combat or not. When it is in combat, it shoots an arrow at the target and possibly waits for a set time between shooting.  

Behavior Trees are great if you need in depth logic that may have a lot of different conditions or actions that need to be taken. Behavior Tree’s are meant to be designed modularly, which may seem daunting at first (you will have lots of different nodes) but in the end leads to three major benefits: 

1.	**Customization**: The Nodes can be mixed or rearranged to create different behaviors with little to no need to recode the inner logic.
2.	**Easier Maintenance**: Each node returns a status code (success, failure or running) which provides a high-level view into the execution of the logic. This makes it much easier to isolate any potential issues.
3.	**Scalable**:  While it’s important to have some decent debugging tools, behavior trees are made to scale and do it very well.

One additional difference is that inherently, state machines are meant to be singular in execution whereas behavior trees can be designed to run different branches in parallel. 

< I found this book, [Artificial Intelligence for Games 2nd Edition](https://www.amazon.com/Artificial-Intelligence-Games-Ian-Millington/dp/0123747317), to be very helpful when I started to learn more about AI for games.>

### [Previous (Introduction)](../readme.md)    |     [Next (Get the touch input)](./pt-2-getting-touch-input.md)
