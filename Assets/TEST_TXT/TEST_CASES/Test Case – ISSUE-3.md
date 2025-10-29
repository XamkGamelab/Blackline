# Test Case – ISSUE-3: Rigidbody Collision with Ground in Play Mode



## Related issue: None

## Priority: Medium

## Type: PlayMode



#### Preconditions



#### Scene/Prefab: None (objects are created at runtime)

#### Data/Config: Default physics settings (gravity, collision layers)

#### Environment: Play Mode in Unity (i.e. full physics simulation)



#### Steps (Given–When–Then)



##### Test — Rigidbody\_Falls\_And\_Collides\_With\_Ground



Given a ground object (cube) positioned at y = –0.5 and scaled to serve as a surface, with its renderer disabled, plus a sphere object at y = 2.0 with a Rigidbody and a CollisionRecorder component.



When the simulation runs for up to maxFixedSteps (200) physics fixed updates (using WaitForFixedUpdate) until the collision is detected or the limit is reached.



Then the CollisionRecorder.collided flag should be true (i.e. the sphere should collide with the ground).



##### Notes



###### Edge cases:



Cant think of any



###### Cleanup:



Destroy both the sphere and ground game objects via Object.Destroy(...)

Yield a final null to allow teardown in coroutine



###### Expected Result Summary:



Sphere should fall under gravity and collide with ground within a bounded number of physics steps

Test passes if recorder.collided == true

