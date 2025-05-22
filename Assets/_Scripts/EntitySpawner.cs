using Unity.VisualScripting;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{


    public void SpawnAnimal(Vector3 position, AnimalObject animalObject)
    {
        GameObject animal = new(animalObject.name);
        animal.transform.position = position;
        Animal animalScript = animal.AddComponent<Animal>();
        animalScript.Initialize(animalObject);
    }

    public void SpawnPlant(Vector3 position, PlantObject plantObject) 
    {
        GameObject plant = new(plantObject.name);
        plant.transform.position = position;
        Plant plantScript = plant.AddComponent<Plant>();
        plantScript.Initialize(plantObject);
    }
}
