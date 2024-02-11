using UnityEngine;

[CreateAssetMenu(fileName = "New Card Data", menuName = "CardData", order = 0)]
public class CardData : ScriptableObject {
    public int cost;
    public int attack;
    public int health;
    
    public static CardData DefaultCard => Resources.Load<CardData>("CardData/default");
    public static CardData DefaultCard2 => Resources.Load<CardData>("CardData/default2");
    public static CardData DefaultCard3 => Resources.Load<CardData>("CardData/default3");
}