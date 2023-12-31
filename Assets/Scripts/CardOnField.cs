using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CardOnField : MonoBehaviour {
    public EventHandler OnCardDestroyed;
    
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text costText;
    
    [FormerlySerializedAs("stats")]
    [Header("Card Stats")]
    [SerializeField] private CardData data;
    
    private int _playerIndex;
    private int _attack;
    private int _health;
    private int _cost;
    
    private void Start() {
        SetData(data);
    }

    public void Attack(CardOnField target) {
        if (target._playerIndex == _playerIndex) {
            Debug.Log("Cannot attack your own card!");
            return;
        }
        TakeDamage(target._attack);
        target.TakeDamage(_attack);
    }

    protected void TakeDamage(int damage) {
        _health -= damage;
        healthText.text = _health.ToString();
        if (_health <= 0) {
            OnCardDestroyed?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject); // TODO: replace with proper death handling
        }
    }
    
    public void SetData(CardData cardData) {
        data = cardData;
        _cost = cardData.cost;
        _attack = cardData.attack;
        _health = cardData.health;
        costText.text = _cost.ToString();
        attackText.text = _attack.ToString();
        healthText.text = _health.ToString();
    }
    
    public int GetPlayerIndex() {
        return _playerIndex;
    }
    
    public void SetPlayerIndex(int index) {
        _playerIndex = index;
    }
}