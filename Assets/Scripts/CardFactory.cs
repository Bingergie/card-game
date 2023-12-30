using UnityEngine;

public static class CardFactory {
    public static Card CreateCard(CardStats stats) {
        var gameObject = Object.Instantiate(Resources.Load<GameObject>("CardPrefabs/Card"));
        var card = gameObject.GetComponent<Card>();
        card.SetStats(stats);
        return card;
    }
}