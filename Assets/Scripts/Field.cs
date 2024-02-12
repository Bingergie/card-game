using System;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {
    public const int MaxCards = 5;

    private static Field[] _fields = new Field[2];

    [SerializeField] private float spacing = 1.0f;
    [SerializeField] private int playerIndex;

    private List<CardOnField> _cards = new List<CardOnField>();
    public bool IsFull => _cards.Count >= MaxCards;

    private void Start() {
        // todo: remove this
        AddCard(new Card(CardData.DefaultCard));
        AddCard(new Card(CardData.DefaultCard2));
        AddCard(new Card(CardData.DefaultCard3));
    }
    
    public static Field CreateField(int playerIndex) {
        var field = Instantiate(Resources.Load<Field>("CardPrefabs/Field"));
        field.playerIndex = playerIndex;
        _fields[playerIndex] = field;
        return field;
    }

    public static Field GetField(int playerIndex) {
        return _fields[playerIndex];
    }

    public void AddCard(Card card1) {
        if (_cards.Count >= MaxCards) {
            Debug.LogError("Cannot add more cards to field! id: " + playerIndex);
            return;
        }

        var card = CardOnField.CreateCard(card1, playerIndex);
        _cards.Add(card);
        card.transform.SetParent(transform);
        card.OnCardDestroyed += HandleOnCardDestroyed;
        RearrangeCards();
    }

    private void HandleOnCardDestroyed(object sender, EventArgs e) {
        var card = (CardOnField)sender;
        _cards.Remove(card);
        RearrangeCards();
    }

    private void RearrangeCards() {
        for (var i = 0; i < _cards.Count; i++) {
            var j = i - _cards.Count / 2f + 0.5f;
            var cardTransform = _cards[i].transform;
            cardTransform.position = transform.position + new Vector3(j * spacing, 0, 0);
        }
    }
}