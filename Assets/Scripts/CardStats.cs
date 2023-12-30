using UnityEngine;

[CreateAssetMenu(fileName = "New Card Stats", menuName = "CardStats", order = 0)]
public class CardStats : ScriptableObject {
    public int cost;
    public int attack;
    public int health;
}