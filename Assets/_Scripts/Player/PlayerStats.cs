using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int money;

    public bool CanAfford(int cost)
    {
        return money >= cost;
    }

    public void Buy(int cost)
    {
        money -= cost;
    }
}
