using System;

public class Card { // stores the stats of a card and any effects and changes to attack/health/cost
    public EventHandler OnStatsChanged;

    public CardData Data { get; private set; }
    public int Attack { get; private set; }
    public int MaxHealth { get; private set; }
    public int Cost { get; private set; }

    public Card(CardData data) {
        SetData(data);
    }

    public void SetData(CardData cardData) {
        Data = cardData;
        Attack = Data.attack;
        MaxHealth = Data.health;
        Cost = Data.cost;
        OnStatsChanged?.Invoke(this, EventArgs.Empty);
    }
}