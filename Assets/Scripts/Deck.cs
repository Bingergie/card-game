using System.Collections.Generic;
using UnityEngine;

public class Deck {
    public const int DeckSize = 30;
    private List<Card> _cards; // todo: make into dictionary for faster lookup

    public static Deck DefaultDeck {
        get {
            var deck = new Deck(new List<CardData>());
            for (int i = 0; i < DeckSize / 3; i++) {
                deck._cards.Add(new Card(CardData.DefaultCard));
                deck._cards.Add(new Card(CardData.DefaultCard2));
                deck._cards.Add(new Card(CardData.DefaultCard3));
            }
            return deck.Shuffle();
        }
    }
    
    public static bool IsDeckValid(Deck deck) {
        if (deck._cards.Count != DeckSize) return false;
        var cardCount = new Dictionary<CardData, int>();
        foreach (var card in deck._cards) {
            if (cardCount.ContainsKey(card.Data)) {
                cardCount[card.Data]++;
            } else {
                cardCount[card.Data] = 1;
            }
        }
        return true;
    }
    
    public Deck(List<CardData> cards) {
        _cards = new List<Card>(cards.Count);
        foreach (var cardData in cards) {
            if (cardData == null) {
                Debug.LogError("CardData is null! replacing with default deck. ");
                _cards = DefaultDeck._cards;
                break;
            }
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