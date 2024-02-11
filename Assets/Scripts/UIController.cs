using System;
using UnityEngine;

public class UIController : Singleton<UIController> {
    [SerializeField] private int activePlayerIndex = 0;
    
    [SerializeField] private Transform playerCharacterLocation;
    [SerializeField] private Transform opponentCharacterLocation;
    [SerializeField] private Transform playerFieldLocation;
    [SerializeField] private Transform opponentFieldLocation;
    
    private PlayerCharacter playerCharacter;
    private PlayerCharacter opponentCharacter;
    private Field playerField;
    private Field opponentField;

    protected override void Awake() {
        base.Awake();
        GameController.Instance.OnGameStart += SpawnUI;
    }

    private void SpawnUI(object sender, EventArgs e) {
        var playerIndex = 0;
        
        var player = PlayerCharacter.CreateCharacter(playerIndex);
        player.transform.position = playerCharacterSpawn.position;
        var opponent = PlayerCharacter.CreateCharacter(1 - playerIndex);
        opponent.transform.position = opponentCharacterSpawn.position;
        
        var playerField = Field.CreateField(playerIndex);
        playerField.transform.position = playerFieldSpawn.position;
        var opponentField = Field.CreateField(1 - playerIndex);
        opponentField.transform.position = opponentFieldSpawn.position;
    }
}