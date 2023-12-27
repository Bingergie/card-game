using System;
using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(Camera))]
public class Player : MonoBehaviour {
    private Camera _camera;

    [CanBeNull] private Card _selectedCard;

    private void Awake() {
        _camera = GetComponent<Camera>();
        var input = GetComponent<PlayerInput>();
        input.OnMouseDown += InputOnMouseDown;
        input.OnMouseUp += InputOnMouseUp;
        input.OnMouseMove += InputOnMouseMove;
    }

    private void InputOnMouseDown(object sender, Vector3 mousePosition) {
        var ray = _camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out var hit, 1000)) {
            var card = hit.collider.GetComponent<Card>();
            if (card != null) { // todo: check if card is on player's side
                _selectedCard = card; // todo: highlight selected card
                Debug.Log("Selected card " + card.name);
            }
        }
    }

    private void InputOnMouseUp(object sender, Vector3 mousePosition) {
        var ray = _camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out var hit)) {
            var card = hit.collider.GetComponent<Card>();
            if (_selectedCard != null && card != null && card != _selectedCard) { // todo: check if card is on opponent's side
                _selectedCard.Attack(card);
                _selectedCard = null; // todo: unhighlight selected card
                Debug.Log("Attacked card " + card.name);
            }
        }
    }

    private void InputOnMouseMove(object sender, Vector3 mousePosition) {
    }
}