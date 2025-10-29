# Test Case – ISSUE-2: Scene Load Button Behavior (EditMode Validation)



## Related issue: None

## Priority: Low

## Type: EditMode



#### Preconditions

#### 

#### Scene/Prefab: N/A (runtime-generated GameObject)

#### Data/Config: Mocked subclass TestableSceneLoadButton (prevents actual SceneManager.LoadScene calls)

#### Environment: Runs in Edit Mode; verifies method logic only, not scene transitions.



##### Steps (Given–When–Then)

##### 

##### Test 1 — OnButtonPressed\_WithValidIndex\_RecordsIndex



Given a GameObject with the mock component TestableSceneLoadButton.

When OnButtonPressed(1) is called.

Then the component’s LastSceneIndex should record the provided index (1), confirming correct parameter handling.



##### Test 2 — OnButtonPressed\_WithInvalidIndex\_RecordsIndex



Given a GameObject with the mock component TestableSceneLoadButton.

When OnButtonPressed(-9999) is called (invalid index).

Then the component’s LastSceneIndex should still record the provided invalid index (-9999), verifying that input is passed and stored even without validation or actual scene load.



##### Notes

##### 

##### Edge cases:

##### 

Failure because of error



##### Cleanup:



All temporary GameObjects are destroyed immediately using Object.DestroyImmediate().



##### Expected Result Summary:



OnButtonPressed() correctly records input values regardless of validity.

Actual scene loading behavior is intentionally bypassed for safe EditMode testing.

