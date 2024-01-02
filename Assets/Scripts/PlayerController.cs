using System.Collections.Generic;
using System.Linq;
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
            var targetCard = hit.collider.GetComponent<CardOnField>();
            if (_selectedCardOnField != null && targetCard != null && 
                targetCard.PlayerIndex != _selectedCardOnField.PlayerIndex) {
                _player.Attack(_selectedCardOnField, targetCard);
                _selectedCardOnField = null; // todo: unhighlight selected card
                Debug.Log("Attacked card " + targetCard.name);
            }
        }
    }

    private void InputOnMouseMove(object sender, Vector3 mousePosition) {
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            _player.PlayCard(_player.DrawCard());
        }
    }
}