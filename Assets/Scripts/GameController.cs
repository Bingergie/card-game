using System;
using UnityEngine;

public class GameController : Singleton<GameController> {
    public EventHandler<int> OnPlayerWin;

    private void Start() {
        UIController.Instance.SpawnUI(0);
        OnPlayerWin += HandleWin;
    }

    private void HandleWin(object sender, int e) {
        Debug.Log("Player " + e + " wins!");
    }

    public void HandleAttack(CardOnField attacker, CardOnField defender) { // todo: make this queue attack instead
        attacker.TakeDamage(defender.Attack);
        defender.TakeDamage(attacker.Attack);
    }
    
    public void HandleAttack(CardOnField attacker, PlayerCharacter defender) {
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