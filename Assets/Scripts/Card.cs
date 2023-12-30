using TMPro;
using UnityEngine;

public class Card : MonoBehaviour {
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
        // set atk, hp, cost from scriptable object
        _cost = stats.cost;
        _attack = stats.attack;
        _health = stats.health;
        // update text
        costText.text = _cost.ToString();
        attackText.text = _attack.ToString();
        healthText.text = _health.ToString();
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
            Destroy(gameObject); // TODO: replace with proper death handling
        }
    }
}