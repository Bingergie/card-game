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
    
    [FormerlySerializedAs("stats")]
    [Header("Card Stats")]
    [SerializeField] private CardData data;
    
    public int PlayerIndex { get; private set; }
    public int Attack => _card.Attack;
    public int Health => _damageableEntity.Health;
    public int Cost => _card.Cost;
        
    private Card _card;
    private DamageableEntity _damageableEntity;
    
    public static CardOnField CreateCard(Card cardObject, int playerIndex) {
        var gameObject = Instantiate(Resources.Load<GameObject>("CardPrefabs/Card"));
        var card = gameObject.GetComponent<CardOnField>();
        card.PlayerIndex = playerIndex;
        card._card = cardObject;
        return card;
    }
    
    private void Awake() {
        if (_card == null) {
            _card = new Card(Resources.Load<CardData>("CardData/default"));
        }
        _damageableEntity = GetComponent<DamageableEntity>();
        _damageableEntity.SetHealthToMax(data.health);
        _damageableEntity.OnDeath += HandleDeath;
    }
    
    private void Start() {
        SetData(data);
    }

    public void TakeDamage(int damage) {
        _damageableEntity.TakeDamage(damage);
        healthText.text = Health.ToString();
    }
    
    private void HandleDeath(object sender, EventArgs e) {
        OnCardDestroyed?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject); // TODO: replace with proper death handling
    }
    
    public void SetData(CardData cardData) {
        data = cardData;
        _damageableEntity.SetHealthToMax(cardData.health);
        costText.text = Cost.ToString();
        attackText.text = Attack.ToString();
        healthText.text = Health.ToString();
    } 
}