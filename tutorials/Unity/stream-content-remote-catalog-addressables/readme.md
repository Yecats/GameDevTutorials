> ## View the entire tutorial on [GameDev Resources](https://gamedev-resources.com/).
> 
# Introduction

Having the ability to load content into a game without forcing the user to download an update is critical to keeping players engaged. There are a lot of different use cases for this. You might want to swap out some models and textures so that they match the holiday season. Or, you can also use this technique to reduce the initial download time of your game by packaging only a couple of levels and assets. Then you can rely on Addressables and a CDN to download the remaining game behind the scenes while they are playing.

In this tutorial, you’ll expand on your Addressables knowledge from part 1 of this series and learn how to read from a remote catalog hosted on Unity’s Cloud Content Delivery (CCD) service.

![Example](https://gamedev-resources.com/wp-content/uploads/2021/07/Test-Final_1024x576.gif)

## Learning Outcomes
In this part of the series, you will learn how to:

1. Configure Addressables to build for and load from a remote catalog.
2. How to configure Addressables to work with multiple platforms.
3. Understand the concepts of Cloud Content Delivery (CCD) and how to set up buckets/releases.
4. Upload your addressable assets to Cloud Content Delivery.
5. How to switch the assets that are streamed into your game on launch.

## Prerequisites

> Tutorials may work with earlier versions. The version referenced is the on I used.

1. Basic knowledge of Addressables and/or completion of [Load, unload and change assets at runtime with Addressables](https://gamedev-resources.com/load-unload-and-change-assets-at-runtime-with-addressables/).
2.	You need [Unity 2020.3 (LTS)](https://unity3d.com/get-unity/download) or later to follow along with this tutorial.
3.	This tutorial assumes you already have basic knowledge of Unity and intermediate knowledge of C#.

