using UnityEngine;

[CreateAssetMenu(fileName = "Plant", menuName = "Scriptable Objects/Plant")]
public class PlantObject : ScriptableObject
{
    public enum PlantType
    {
        Flower,
        Tree,
        Shrub,
        Grass
    }
    
    public enum PlantSize
    {
        Small,
        Medium,
        Large
    }
}
