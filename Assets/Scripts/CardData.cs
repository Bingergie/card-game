using UnityEngine;

[CreateAssetMenu(fileName = "New Card Data", menuName = "CardData", order = 0)]
public class CardData : ScriptableObject {
    public int cost;
    public int attack;
    public int health;
}