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
        GameController.Instance.OnGameStart += HandleOnGameStart;
    }

    public void SwitchActivePlayer() {
        Debug.Log("Switching active player");
        activePlayerIndex = 1 - activePlayerIndex;
        
        (playerCharacter, opponentCharacter) = (opponentCharacter, playerCharacter);
        (playerField, opponentField) = (opponentField, playerField);
        
        UpdateCharacterAndFieldPositions();
    }

    private void HandleOnGameStart(object sender, EventArgs e) {
        playerCharacter = PlayerCharacter.CreateCharacter(activePlayerIndex);
        opponentCharacter = PlayerCharacter.CreateCharacter(1 - activePlayerIndex);
        
        playerField = Field.CreateField(activePlayerIndex);
        opponentField = Field.CreateField(1 - activePlayerIndex);
        
        UpdateCharacterAndFieldPositions();
    }
    
    private void UpdateCharacterAndFieldPositions() {
        Debug.Log("Updating character and field positions");
        playerCharacter.transform.position = playerCharacterLocation.position;
        opponentCharacter.transform.position = opponentCharacterLocation.position;

        playerField.transform.position = playerFieldLocation.position;
        opponentField.transform.position = opponentFieldLocation.position;
    }
}