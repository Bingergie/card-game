using System.Collections.Generic;
using UnityEngine;

public class Player {
    public List<CardStats> Deck;

    private Deck _remainingDeck;
    private List<CardOnField> _hand = new List<CardOnField>();
    private List<CardOnField> _field = new List<CardOnField>();

    public Player(List<CardStats> deck) {
        Deck = deck;
        _remainingDeck = new Deck(deck);
    }

    public void Attack(CardOnField attacker, CardOnField target) {
        attacker.Attack(target);
    }

    public CardStats DrawCard() {
        var card = _remainingDeck.Draw();
        _hand.Add(CardFactory.CreateCard(card, 0));
        return card;
    }
}

public class Deck {
    private List<CardStats> _cards;

    public Deck(List<CardStats> cards) {
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

    public CardStats Draw() {
        if (_cards.Count <= 0) {
            Debug.Log("No more cards in deck!");
            return null; // or throw exception
        }

        var card = _cards[^1]; // get last element
        _cards.RemoveAt(0);
        return card;
    }
}