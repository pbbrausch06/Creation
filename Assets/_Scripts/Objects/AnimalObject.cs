using UnityEngine;
using UnityEngine.Rendering;
using static PlantObject;

[CreateAssetMenu(fileName = "Animal", menuName = "Scriptable Objects/Animal")]
public class AnimalObject : ScriptableObject
{
    public enum AnimalSpecies
    {
        Mammal,
        Fish,
        Bird,
        Reptile,
        Amphibian
    }

    public enum AnimalSize
    {
        Tiny,
        Small,
        Medium,
        Large
    }

    public enum AnimalDiet
    {
        Herbivore,
        Carnivore,
        Omnivore
    }

    [Header("General Settings")]
    public AnimalSpecies Type { get; private set; }
    public AnimalDiet Diet { get; private set; }
    public AnimalSize Size { get; private set; }
    public BiomeObject[] SpawnableBiomes { get; private set; }

    [Header("Appearance")]
    public Sprite Icon { get; private set; }
    public GameObject Model { get; private set; }

    [Header("Vital Stats")]
    public int MaxHealth { get; private set; }
    public int Lifespan { get; private set; }
    public float MinimumSpeed { get; private set; }
    public float MaximumSpeed { get; private set; }
    public float Acceleration { get; private set; }
    public float TurningSpeed { get; private set; }
    public float VisionRange { get; private set; }
    public float GrowthRate { get; private set; }
    public float MinimumScale { get; private set; }
    public float MaximumScale { get; private set; }

    [Header("Reproduction")]
    public float MaturityAge { get; private set; }
    public float ReproductionRate { get; private set; }

    [Header("Diet & Consumption")]
    public AnimalSize MaximumEatableAnimalSize { get; private set; }
    public AnimalObject[] PreferredAnimals { get; private set; }
    public PlantSize MaximumEatablePlantSize { get; private set; }
    public PlantObject[] PreferredPlants { get; private set; }

    [Header("Physiology")]
    public int FoodCapacity { get; private set; }
    public int WaterCapacity { get; private set; }
    public float WaterLossRate { get; private set; }
    public float MetabolismRate { get; private set; }
    public int HealthRecoveryRate { get; private set; }
    public int HealthLossRate {  get; private set; }
    public float EatingRate { get; private set; }
    public float DrinkingRate { get; private set; }
    public float MinTemperature { get; private set; }
    public float MaxTemperature { get; private set; }

    [Header("Behavior & Mutation")]
    public float Aggression { get; private set; }
    public float Fear { get; private set; }
    public float MutationChance { get; private set; }
    public float MutationSpread { get; private set; }
    public float MutationVariation {  get; private set; }
}
