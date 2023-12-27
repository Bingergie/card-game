using TMPro;
using UnityEngine;

public class Card : MonoBehaviour {
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text costText;

    [Header("Card Stats")] // todo: replace with scriptable object
    [SerializeField] private int attack;
    [SerializeField] private int health;
    [SerializeField] private int cost;
    
    

    private void Start() {
        // todo: set atk, hp, cost from scriptable object

        attackText.text = attack.ToString();
        healthText.text = health.ToString();
        costText.text = cost.ToString();
    }

    public void Attack(Card target) {
        TakeDamage(target.attack);
        target.TakeDamage(attack);
    }

    protected void TakeDamage(int damage) {
        health -= damage;
        healthText.text = health.ToString();
        if (health <= 0) {
            Destroy(gameObject); // TODO: replace with proper death handling
        }
    }
}