using UnityEngine;

[CreateAssetMenu(fileName = "BiomeObject", menuName = "Scriptable Objects/BiomeObject")]
public class BiomeObject : ScriptableObject
{
    public enum BiomeType
    {
        Grassland,
        Woodland,
        Rainforest,
        Wetland,
        Desert,
        Tundra,
        Coral_Reef
    }

    public BiomeType Type { get; private set; }
    public float ResourcePerAreaUnit { get; private set; }
    public Color OverlayColor { get; private set; }
}
