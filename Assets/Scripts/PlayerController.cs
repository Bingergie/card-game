using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(Camera))]
public class PlayerController : MonoBehaviour {
    public List<CardData> deck;

    private Camera _camera;
    private Player _player;

    [CanBeNull] private CardOnField _selectedCardOnField;

    private void Awake() {
        for (int i = 0; i < deck.Count; i++) {
            if (deck[i] == null) {
                Debug.LogError("Card data is null! id: " + i);
                deck[i] = Resources.Load<CardData>("CardData/default");
            }
        }

        _camera = GetComponent<Camera>();
        var input = GetComponent<PlayerInput>();
        input.OnMouseDown += InputOnMouseDown;
        input.OnMouseUp += InputOnMouseUp;
        input.OnMouseMove += InputOnMouseMove;
        GameController.Instance.OnGameStart += OnGameStart;
    }

    private void OnGameStart(object sender, int playerIndex) {
        _player = new Player(deck, playerIndex);
    }

    private void InputOnMouseDown(object sender, Vector3 mousePosition) {
        if (!_player.IsTurn) return;

        var ray = _camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out var hit, 1000)) {
            var card = hit.collider.GetComponent<CardOnField>();
            if (card != null && card.PlayerIndex == _player.PlayerIndex) {
                _selectedCardOnField = card; // todo: highlight selected card
                Debug.Log("Selected card " + card.name);
            }
        }
    }

    private void InputOnMouseUp(object sender, Vector3 mousePosition) {
        var ray = _camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out var hit)) {
            if (_selectedCardOnField == null) return;

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("CardOnField")) {
                var targetCard = hit.collider.GetComponent<CardOnField>();
                if (targetCard == null) {
                    Debug.LogError("No cardOnField component on object! id: " + hit.collider.name);
                    return;
                }

                if (targetCard.PlayerIndex == _selectedCardOnField.PlayerIndex) return;

                _player.Attack(_selectedCardOnField, targetCard);
                Debug.Log("Attacked card " + targetCard.name);
                return;
            }

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("PlayerCharacter")) {
                var playerCharacter = hit.collider.GetComponent<PlayerCharacter>();
                if (playerCharacter == null) {
                    Debug.LogError("No playerCharacter component on object! id: " + hit.collider.name);
                    return;
                }

                if (playerCharacter.PlayerIndex == _selectedCardOnField.PlayerIndex) return;

                _player.Attack(_selectedCardOnField, playerCharacter);
                Debug.Log("Attacked player character " + playerCharacter.name);
                return;
            }
        }

        _selectedCardOnField = null; // todo: unhighlight selected card
    }

    private void InputOnMouseMove(object sender, Vector3 mousePosition) {
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            _player.PlayCard(_player.DrawCard());
        }
    }
}