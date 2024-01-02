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

    private void Awake() {
        _fields[playerIndex] = this;
    }

    private void Start() {
        // todo: remove this
        var card1 = CardFactory.CreateCard(Resources.Load<CardData>("CardData/default"), playerIndex);
        var card2 = CardFactory.CreateCard(Resources.Load<CardData>("CardData/default2"), playerIndex);
        var card3 = CardFactory.CreateCard(Resources.Load<CardData>("CardData/default3"), playerIndex);
        AddCard(card1);
        AddCard(card2);
        AddCard(card3);
    }

    public static Field GetField(int playerIndex) {
        return _fields[playerIndex];
    }

    public void AddCard(CardOnField card) {
        if (_cards.Count >= MaxCards) {
            Debug.LogError("Cannot add more cards to field! id: " + playerIndex);
            return;
        }

        _cards.Add(card);
        card.OnCardDestroyed += CardOnCardDestroyed;
        RearrangeCards();
    }

    private void CardOnCardDestroyed(object sender, EventArgs e) {
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