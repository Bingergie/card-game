using System.Collections.Generic;
using UnityEngine;

public class Player {
    public List<CardData> Deck;

    private Deck _remainingDeck;
    private List<CardOnField> _hand = new List<CardOnField>();
    private List<CardOnField> _field = new List<CardOnField>();

    public Player(List<CardData> deck) {
        Deck = deck;
        _remainingDeck = new Deck(deck);
    }

    public void Attack(CardOnField attacker, CardOnField target) {
        GameController.Instance.HandleAttack(attacker, target);
    }

    public CardData DrawCard() {
        var card = _remainingDeck.Draw();
        _hand.Add(CardFactory.CreateCard(card, 0));
        return card;
    }
}

public class Deck {
    private List<CardData> _cards;

    public Deck(List<CardData> cards) {
        _cards = cards;
    }

    public static void ShuffleDeck(Deck deck) {
        var cards = deck._cards;
        // Fisher-Yates shuffle
        int n = cards.Count;
        while (n > 1) {
            n--;
            int k = new System.Random().Next(n + 1);
            (cards[k], cards[n]) = (cards[n], cards[k]);
        }
    }

    public CardData Draw() {
        if (_cards.Count <= 0) {
            Debug.Log("No more cards in deck!");
            return null; // or throw exception
        }

        var card = _cards[^1]; // get last element
        _cards.RemoveAt(0);
        return card;
    }
}