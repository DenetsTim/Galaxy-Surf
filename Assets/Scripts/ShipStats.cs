using UnityEngine;

public class ShipStats : MonoBehaviour
{
    [SerializeField] private int speed = 2;
    public int Speed { get { return speed; } }

    [SerializeField] private string ship_name;
    public string Name { get { return ship_name; } }

    [SerializeField] private string description;
    public string Description { get { return description; } }

    [SerializeField] private int price = 0;
    public int Price { get { return price; } }
}
