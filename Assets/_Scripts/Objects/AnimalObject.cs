using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Animal", menuName = "Scriptable Objects/Animal")]
public class AnimalObject : ScriptableObject
{
    public enum AnimalType
    {
        Mammal,
        Fish,
        Birds,
        Reptiles,
        Amphibians
    }

    public enum AnimalSize
    {
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

    [Header("Animal Settings")]
    [SerializeField] private AnimalType species;
    [SerializeField] private AnimalDiet diet;
    [SerializeField] private AnimalSize maximumEatableAnimalSize;
    [SerializeField] private PlantObject preferredAnimal;
    [SerializeField] private PlantObject.PlantSize maximumEatablePlantSize;
    [SerializeField] private PlantObject preferredPlant;
    [SerializeField] private Sprite image;
    [SerializeField] private GameObject prefab;
}
