using System;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one GameController in scene!");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void HandleAttack(CardOnField attacker, CardOnField defender) { // todo: make this queue attack instead
        attacker.TakeDamage(defender.Attack);
        defender.TakeDamage(attacker.Attack);
    }

    public CardOnField HandlePlayCard(CardInHand card) {
        var field = Field.GetField(card.PlayerIndex);
        if (field == null) {
            Debug.LogError("No field for player " + card.PlayerIndex);
            return null;
        }
        if (field.IsFull) {
            Debug.LogError("Field is full!");
            return null;
        }
        
        var cardOnField = CardOnField.CreateCard(card.CardObject, card.PlayerIndex);
        field.AddCard(cardOnField);
        Destroy(card.gameObject);
        return cardOnField;
    }
}