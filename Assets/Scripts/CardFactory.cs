using UnityEngine;

public static class CardFactory {
    public static CardOnField CreateCard(CardData data, int playerIndex) {
        var gameObject = Object.Instantiate(Resources.Load<GameObject>("CardPrefabs/Card"));
        var card = gameObject.GetComponent<CardOnField>();
        card.SetPlayerIndex(playerIndex);
        card.SetData(data);
        return card;
    }
}