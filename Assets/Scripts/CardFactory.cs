using UnityEngine;

public static class CardFactory {
    public static Card CreateCard(CardStats stats, int playerIndex) {
        var gameObject = Object.Instantiate(Resources.Load<GameObject>("CardPrefabs/Card"));
        var card = gameObject.GetComponent<Card>();
        card.playerIndex = playerIndex;
        card.SetStats(stats);
        return card;
    }
}