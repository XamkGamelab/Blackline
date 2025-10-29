# Test Case – ISSUE-1: <title>

### Related issue:

### Priority: Low

### Type: EditMode



#### Preconditions



#### -Scene/Prefab: None (runtime-generated GameObject)

#### -Data/Config: Uses Unity’s built-in Physics system and Rigidbody component

#### -Environment: Physics simulation manually advanced with Physics.Simulate() in Edit Mode



### Steps (Given–When–Then)



##### Test 1 — Rigidbody\_Falls\_When\_Gravity\_Enabled



Given a new GameObject with a Rigidbody (useGravity = true, isKinematic = false) positioned at y = 10.

When physics simulation runs for 50 fixed steps with gravity enabled.

Then the object’s Y-position should decrease, confirming that it fell under gravity.



##### Test 2 — Rigidbody\_DoesNotFall\_When\_Gravity\_Disabled



Given a GameObject with a Rigidbody (useGravity = true) positioned at y = 10, but global Physics.gravity set to Vector3.zero.

When physics simulation runs for 50 fixed steps.

Then the object’s Y-position should remain effectively the same (within tolerance 1e-4f), confirming that no gravity-induced movement occurred.



##### Test 3 — GravityTestWithEnumeratorPasses



Given a new GameObject with a Rigidbody under gravity, starting at y = 5.

When a single Physics.Simulate() step is executed, followed by a yield return null.

Then the object’s Y-position should decrease, indicating downward motion under gravity within one fixed step.



##### Notes

##### 

##### Edge cases:



Couldn't Verify behavior when Physics.autoSimulation is toggled back on after tests.



##### Cleanup:



Destroy all temporary GameObjects immediately using Object.DestroyImmediate().



Restore original values for Physics.autoSimulation and Physics.gravity.



##### Expected Result Summary:



With gravity → object falls.



Without gravity → object remains stationary.



Enumerator variant → single-step fall verified.

