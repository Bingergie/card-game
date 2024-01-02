using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(DamageableEntity))]
public class CardOnField : MonoBehaviour {
    public EventHandler OnCardDestroyed;

    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text costText;

    public int PlayerIndex { get; private set; }
    public int Attack => CardObject.Attack;
    public int MaxHealth => CardObject.MaxHealth;
    public int Health => _damageableEntity.Health;
    public int Cost => CardObject.Cost;

    public Card CardObject { get; private set; }
    private DamageableEntity _damageableEntity;

    public static CardOnField CreateCard(Card cardObject, int playerIndex) {
        var card = Instantiate(Resources.Load<CardOnField>("CardPrefabs/CardOnField"));
        card.PlayerIndex = playerIndex;
        card.SetCard(cardObject);
        return card;
    }

    private void Awake() {
        if (CardObject == null) {
            CardObject = new Card(Resources.Load<CardData>("CardData/default"));
        }

        _damageableEntity = GetComponent<DamageableEntity>();
        _damageableEntity.OnDeath += HandleDeath;
        
        SetCard(CardObject);
    }
    
    private void SetCard(Card card) {
        CardObject = card;
        _damageableEntity.SetHealthToMax(MaxHealth);
        costText.text = Cost.ToString();
        attackText.text = Attack.ToString();
        healthText.text = Health.ToString();
    }

    public void TakeDamage(int damage) {
        _damageableEntity.TakeDamage(damage);
        healthText.text = Health.ToString();
    }

    private void HandleDeath(object sender, EventArgs e) {
        OnCardDestroyed?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject); // TODO: replace with proper death handling
    }
}