using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(PlayerInput), typeof(Camera))]
public class PlayerController : MonoBehaviour {
    private Camera _camera;
    [SerializeField] private Player player;

    [CanBeNull] private CardOnField _selectedCardOnField;

    private void Awake() {
        if (player == null) {
            Debug.LogError("Player not set!");
        }

        _camera = GetComponent<Camera>();
        var input = GetComponent<PlayerInput>();
        input.OnMouseDown += InputOnMouseDown;
        input.OnMouseUp += InputOnMouseUp;
        input.OnMouseMove += InputOnMouseMove;
    }

    private void Start() {
        TurnManager.Instance.OnTurnStart += HandleTurnStart;
    }

    private void HandleTurnStart(object sender, int e) {
        gameObject.SetActive(player.IsTurn);
    }

    private void InputOnMouseDown(object sender, Vector3 mousePosition) {
        if (!player.IsTurn) return;

        var ray = _camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out var hit, 1000)) {
            var card = hit.collider.GetComponent<CardOnField>();
            if (card != null && card.PlayerIndex == player.PlayerIndex) {
                _selectedCardOnField = card; // todo: highlight selected card
                Debug.Log("Selected card " + card.name);
            }
        }
    }

    private void InputOnMouseUp(object sender, Vector3 mousePosition) {
        if (_selectedCardOnField == null) return;

        var ray = _camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out var hit)) {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("CardOnField")) {
                var targetCard = hit.collider.GetComponent<CardOnField>();
                if (targetCard == null) {
                    Debug.LogError("No cardOnField component on object! id: " + hit.collider.name);
                    return;
                }

                if (targetCard.PlayerIndex == _selectedCardOnField.PlayerIndex) return;

                player.Attack(_selectedCardOnField, targetCard);
                Debug.Log("Attacked card " + targetCard.name);
            }
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("PlayerCharacter")) {
                var playerCharacter = hit.collider.GetComponent<PlayerCharacter>();
                if (playerCharacter == null) {
                    Debug.LogError("No playerCharacter component on object! id: " + hit.collider.name);
                    return;
                }

                if (playerCharacter.PlayerIndex == _selectedCardOnField.PlayerIndex) return;

                player.Attack(_selectedCardOnField, playerCharacter);
                Debug.Log("Attacked player character " + playerCharacter.name);
            }
        }

        _selectedCardOnField = null; // todo: unhighlight selected card
    }

    private void InputOnMouseMove(object sender, Vector3 mousePosition) {
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            player.PlayCard(player.DrawCard());
        }
    }
}