# Challenge: Quick Slots
There are a lot of really interesting ways that you can extend out the inventory system! A great challenge would be to add five quick slots at the bottom of the screen. Here’s how the logic could work:

1. If the player drags an item from the Inventory to the Quick Slot, it gets added to the Quick slot, but not deleted from the Inventory. 
2. If the player drags an item from the Quick Slot back to the inventory, it’s only removed from the QuickSlot.
3. The player can reorder Quick Slot items. 
4. The player can lock the Quick Slot bar so that they can only edit it when the Inventory window is visible. 

Here’s a few things to keep in mind:

1. You can create more UI documents within the scene - just attach the same Panel Settings to each one.
2. Avoid disabling the GameObject that has the UI Document as a way to toggle the window off. Instead, toggle `Style.Display` or `Style.Visibility` on the UI Document’s root element.

Have fun with the challenge and be sure to reach out on [Twitter](https://twitter.com/yecats131) or the [GitHub Discussion Forum](https://github.com/Yecats/GameDevTutorials/discussions) to show me what you've made!

### [Previous (Bonus: Samples & Debugging the UI)](./pt6.md)