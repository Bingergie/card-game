using System;
using UnityEngine;

public class GameController : MonoBehaviour {
    public EventHandler<int> OnPlayerWin;
    public static GameController Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one GameController in scene!");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start() {
        SpawnUI();
        OnPlayerWin += HandleWin;
    }
    
    private void SpawnUI() { // todo: move this to a UI controller
        var player = PlayerCharacter.CreateCharacter(0);
        player.transform.position = new Vector3(0, -3.5f, -7.5f);
        var opponent = PlayerCharacter.CreateCharacter(1);
        opponent.transform.position = new Vector3(0, -3.5f, 7.5f);
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