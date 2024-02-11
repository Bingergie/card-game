using System.Collections.Generic;
using UnityEngine;

public class Player {
    public List<CardData> DeckList { get; private set; }
    public readonly int PlayerIndex;

    private Deck _remainingDeck;
    private List<CardInHand> _hand = new List<CardInHand>();
    private List<CardOnField> _field = new List<CardOnField>();

    public bool IsTurn => TurnManager.Instance.CurrentPlayerIndex == PlayerIndex;
    
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