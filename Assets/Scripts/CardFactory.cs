using UnityEngine;

public static class CardFactory {
    public static CardOnField CreateCard(CardStats stats, int playerIndex) {
        var gameObject = Object.Instantiate(Resources.Load<GameObject>("CardPrefabs/Card"));
        var card = gameObject.GetComponent<CardOnField>();
        card.SetPlayerIndex(playerIndex);
        card.SetStats(stats);
        return card;
    }
}