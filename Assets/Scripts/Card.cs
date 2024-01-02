using System;

public class Card { // stores the stats of a card and any effects and changes to attack/health/cost
    public EventHandler OnStatsChanged;

    private CardData _data;
    public int Attack { get; private set; }
    public int MaxHealth { get; private set; }
    public int Cost { get; private set; }

    public Card(CardData data) {
        SetData(data);
    }

    public void SetData(CardData cardData) {
        _data = cardData;
        Attack = _data.attack;
        MaxHealth = _data.health;
        Cost = _data.cost;
        OnStatsChanged?.Invoke(this, EventArgs.Empty);
    }
}