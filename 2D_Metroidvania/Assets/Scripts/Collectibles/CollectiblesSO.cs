using UnityEngine;

public abstract class CollectiblesSO : ScriptableObject
{
    public string itemName;
    public Sprite icon;

    public abstract void Collect(Player player);
}
