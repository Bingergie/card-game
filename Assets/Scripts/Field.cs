using System;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {
    public static int MaxCards = 5;

    [SerializeField] private float spacing = 1.0f;
    [SerializeField] public int playerIndex;
    
    private List<CardOnField> _cards = new List<CardOnField>();

    private void Start() {
        // todo: remove this
        var card1 = CardFactory.CreateCard(Resources.Load<CardStats>("CardStats/default"), playerIndex);
        var card2 = CardFactory.CreateCard(Resources.Load<CardStats>("CardStats/default2"), playerIndex);
        var card3 = CardFactory.CreateCard(Resources.Load<CardStats>("CardStats/default3"), playerIndex);
        AddCard(card1);
        AddCard(card2);
        AddCard(card3);
    }

    public void AddCard(CardOnField card) {
        if (_cards.Count >= MaxCards) {
            Debug.Log("Cannot add more cards to field! id: " + playerIndex);
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