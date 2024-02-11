using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private List<CardData> deckList;
    public List<CardData> DeckList => deckList;
    
    [SerializeField, Range(0, 1)] private int playerIndex;
    public int PlayerIndex => playerIndex;

    [DoNotSerialize] private Deck _remainingDeck;
    [DoNotSerialize] private List<CardInHand> _hand = new List<CardInHand>();
    [DoNotSerialize] private List<CardOnField> _field = new List<CardOnField>();

    public bool IsTurn => TurnManager.Instance.CurrentPlayerIndex == PlayerIndex;

    private void HandleTurnStart(object sender, int e) {
        // todo
        
        // if not my turn, return
        
        // gain mana
        // draw card
    }

    private void Start() {
        _remainingDeck = new Deck(deckList).Shuffle();
        
        TurnManager.Instance.OnTurnStart += HandleTurnStart;
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