using UnityEngine;
using UnityEngine.AI;
using static AnimalObject;
using static PlantObject;

public class Animal : MonoBehaviour
{
    private AnimalObject animalObject;
    private NavMeshAgent agent;

    private int health;
    private float age;
    private float scale;
    private int food;
    private float water;
    private float temperature;
    private float percentMature;
    private GameObject spawnedModel;

    public void Initialize(AnimalObject animalObject)
    {
        this.animalObject = animalObject;

        SetBaseStats();

        spawnedModel = Instantiate(animalObject.Model, transform);

        agent = gameObject.AddComponent<NavMeshAgent>();

        agent.acceleration = animalObject.Acceleration;
        agent.angularSpeed = animalObject.TurningSpeed;

        SimulateMinute();
        SimulateHour();
    }

    private void SetBaseStats()
    {
        health = animalObject.MaxHealth;
        age = 0;
        scale = animalObject.MinimumScale;
        food = animalObject.FoodCapacity / 2;
        water = animalObject.WaterCapacity / 2;
        temperature = (animalObject.MaxTemperature + animalObject.MinTemperature) / 2;
    }

    public void SimulateHour()
    {
        UpdateAge();
        if (percentMature != 1)
        {
            UpdateScale();
            UpdateSpeed(); 
        }
        UpdateHealth();
    }

    public void SimulateMinute()
    {
        CheckDeath();
    }

    private void UpdateAge()
    {
        age += 1f / 24f;
        percentMature = Mathf.Clamp(age / animalObject.MaturityAge, 0f, 1f);
    }

    private void UpdateScale()
    {
        scale = Mathf.Lerp(animalObject.MinimumScale, animalObject.MaximumScale, percentMature);
        transform.localScale = new(scale, scale, scale);
    }

    private void UpdateSpeed()
    {
        agent.speed = Mathf.Lerp(animalObject.MinimumSpeed, animalObject.MaximumSpeed, percentMature);
    }

    private void UpdateHealth()
    {
        int healthToMax = animalObject.MaxHealth - health;

        if (healthToMax == 0) { return; }

        int healthHealed;

        if (food > 0)
        {
            healthHealed = animalObject.HealthRecoveryRate;
            food -= 1;
        }
        else
        {
            healthHealed = -animalObject.HealthLossRate;
        }

        health += healthHealed;
        healthToMax -= healthHealed;

        if (healthToMax == 0) { return; }

        if (water > 0)
        {
            healthHealed = animalObject.HealthRecoveryRate;
            water -= 1;
        }
        else
        {
            healthHealed = -animalObject.HealthLossRate;
        }

        health += healthHealed;
    }

    private void CheckDeath()
    {
        if (health <= 0 || age >= animalObject.Lifespan)
        {

        }
    }
}