# Add physics

For the ball to roll down the ramp, you need to **add a component** that tells it to use Unity’s physics engine:

1. **Select** the **Ball** GameObject and click the **Add Component** button on the **Inspector** window.
2. **Search** for **RigidBody** and push enter to add the component.

> **Important**: Make sure you add the RigidBody component and not the RigidBody2D component. 

A [RigidBody](https://docs.unity3d.com/Manual/class-Rigidbody.html) component can receive force and torque to make an object move in a realistic way. This opens the door to create behaviors such as collisions or in this case the ability to roll down a ramp. Press the **Play** button again to see what happens.

## Colliders

I know, I’ve tricked you again! The ball has mass and responds to gravity, but we haven’t told the ramp or the bucket that they need to act like a hard surface. This is done by adding a [Mesh Collider](https://docs.unity3d.com/Manual/CollidersOverview.html) component to the Ramp and Bucket:

1. **Select** the **Ramp** GameObject and **Mesh Collider** as a new component.
2. **Select** the **Bucket** GameObject and **Mesh Collider** as a new component.

Press the **Play** button again to see what happens.

### [Previous (Getting started)](./pt2-create-the-world.md)    |     [Next (Create the world)](./pt4-display-a-message.md)