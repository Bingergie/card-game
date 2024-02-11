using System;
using UnityEngine;

public class UIController : Singleton<UIController> {
    [SerializeField] private Transform playerCharacterSpawn;
    [SerializeField] private Transform opponentCharacterSpawn;
    [SerializeField] private Transform playerFieldSpawn;
    [SerializeField] private Transform opponentFieldSpawn;

    private void Awake() {
        GameController.Instance.OnGameStart += SpawnUI;
    }

    private void SpawnUI(object sender, int playerIndex) {
        Debug.Log("spawning ui");
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