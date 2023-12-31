using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(PlayerInput),  typeof(Camera))]
public class PlayerController : MonoBehaviour {
    public List<CardStats> deck;
    
    private Camera _camera;
    private Player _player;

    [CanBeNull] private Card _selectedCard;

    private void Awake() {
        _player = new Player(deck);
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
            var targetCard = hit.collider.GetComponent<Card>();
            if (_selectedCard != null && targetCard != null && targetCard.playerIndex != _selectedCard.playerIndex) {
                _player.Attack(_selectedCard, targetCard);
                _selectedCard = null; // todo: unhighlight selected card
                Debug.Log("Attacked card " + targetCard.name);
            }
        }
    }

    private void InputOnMouseMove(object sender, Vector3 mousePosition) {
    }
}