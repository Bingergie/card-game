using System.Collections.Generic;
using UnityEngine;

public class Deck {
    private List<Card> _cards;

    public Deck(List<CardData> cards) {
        _cards = new List<Card>(cards.Count);
        foreach (var cardData in cards) {
            _cards.Add(new Card(cardData));
        }
    }

    public Deck Shuffle() {
        // Fisher-Yates shuffle
        int n = _cards.Count;
        while (n > 1) {
            n--;
            int k = new System.Random().Next(n + 1);
            (_cards[k], _cards[n]) = (_cards[n], _cards[k]);
        }

        return this;
    }

    public Card DrawCard() {
        if (_cards.Count <= 0) {
            Debug.Log("No more cards in deck!");
            return null; // or throw exception
        }

        var card = _cards[^1]; // get last element
        _cards.RemoveAt(0);
        return card;
    }
}