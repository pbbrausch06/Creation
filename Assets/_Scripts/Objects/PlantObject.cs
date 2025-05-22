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

    [Header("General Settings")]
    public PlantType Type { get; private set; }
    public PlantSize Size { get; private set; }
    public BiomeObject[] SpawnableBiomes { get; private set; }

    [Header("Appearance")]
    public Sprite Icon { get; private set; }
    public GameObject Model { get; private set; }

    [Header("Vital Stats")]
    public int MaxHealth { get; private set; }
    public int Lifespan { get; private set; }
    public float GrowthRate { get; private set; }
    public float MinimumScale { get; private set; }
    public float MaximumScale { get; private set; }

    [Header("Reproduction")]
    public float ReproductiveAge { get; private set; }
    public float ReproductionRate { get; private set; }
    public float SeedSpreadRadius { get; private set; }
    public float SeedSpreadRate { get; private set; }

    [Header("Nutrition & Interaction")]
    public float MaxSatiation { get; private set; }

    [Header("Physiology")]
    public int WaterCapacity { get; private set; }
    public float DehydrationRate { get; private set; }
    public float MinTemperature { get; private set; }
    public float MaxTemperature { get; private set; }

    [Header("Behavior & Mutation")]
    public float MutationChance { get; private set; }
    public float MutationSpread { get; private set; }
    public float MutationVariation { get; private set; }
}
