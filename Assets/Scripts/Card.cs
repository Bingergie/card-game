using System;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour {
    public EventHandler OnCardDestroyed;
    
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text costText;
    
    [Header("Card Stats")]
    [SerializeField] private CardStats stats;
    
    [Header("Debug")]
    public int playerIndex;
    
    private int _attack;
    private int _health;
    private int _cost;
    
    private void Start() {
        SetStats(stats);
    }

    public void Attack(Card target) {
        if (target.playerIndex == playerIndex) {
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
    
    public void SetStats(CardStats cardStats) {
        stats = cardStats;
        _cost = cardStats.cost;
        _attack = cardStats.attack;
        _health = cardStats.health;
        costText.text = _cost.ToString();
        attackText.text = _attack.ToString();
        healthText.text = _health.ToString();
    }
}