using System.Collections.Generic;
using UnityEngine;

public class Player {
    public List<CardData> DeckList { get; private set; }
    public readonly int PlayerIndex;

    private Deck _remainingDeck;
    private List<CardInHand> _hand = new List<CardInHand>();
    private List<CardOnField> _field = new List<CardOnField>();

    public Player(List<CardData> deck, int playerIndex = 0) { // todo: remove " = 0"
        DeckList = deck;
        PlayerIndex = playerIndex;
        _remainingDeck = new Deck(deck).Shuffle();
    }

    public void Attack(CardOnField attacker, CardOnField target) {
        GameController.Instance.HandleAttack(attacker, target);
    }
    
    public void Attack(CardOnField attacker, PlayerCharacter target) {
        GameController.Instance.HandleAttack(attacker, target);
    }

    public CardInHand DrawCard() {
        var card = _remainingDeck.DrawCard();
        if (card == null) {
            Debug.Log("No more cards in deck, you lose!");
            GameController.Instance.OnPlayerWin?.Invoke(this, 1 - PlayerIndex);
            return null;
        }
        var cardInHand = CardInHand.CreateCard(card, PlayerIndex);
        _hand.Add(cardInHand);
        return cardInHand;
    }
    
    public void PlayCard(CardInHand card) {
        if (_hand.Find(c => c == card) == null) {
            Debug.LogError("Card not found in hand! id: " + PlayerIndex);
            return;
        }
        
        if (_field.Count >= Field.MaxCards) {
            Debug.LogError("Cannot add more cards to field! id: " + PlayerIndex);
            return;
        }
        
        _hand.Remove(card);
        var cardOnField = GameController.Instance.HandlePlayCard(card);
        _field.Add(cardOnField);
    }
}

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