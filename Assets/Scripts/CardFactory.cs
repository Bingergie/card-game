using UnityEngine;

public class CardFactory {
    public static Card CreateCard(CardStats stats) {
        var gameObject = Resources.Load<GameObject>("CardPrefabs/Card");
        var card = gameObject.AddComponent<Card>();
        card.SetStats(stats);
        return card;
    }
}