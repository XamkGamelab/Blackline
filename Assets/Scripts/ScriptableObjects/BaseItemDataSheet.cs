using UnityEngine;

public abstract class BaseItemDataSheet : ScriptableObject
{
    // This base item data sheet will contain very primitive data, such as an ItemID. //
    // That's good for things like multiplayer later on, so we can use a library of ItemIDs //
    // with corresponding items. We can avoid large amounts of traffic this way, instead of //
    // sending whole GameObjects over the network. -Davoth //
    
    // A byte is enough, right?? Surely we aren't going to have more than 255 different items, right??? -Davoth //
    [Header("Base Item Data")]
    [SerializeField]
    private byte _itemID; 
}
