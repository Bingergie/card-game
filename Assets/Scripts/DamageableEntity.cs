using System;
using UnityEngine;

public class DamageableEntity : MonoBehaviour {
    public EventHandler OnDeath;
    public int MaxHealth { get; private set; }
    public int Health { get; private set; }

    public void TakeDamage(int damage) {
        Health -= damage;

        if (Health <= 0) {
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }
    
    public void SetHealthToMax(int maxHealth) {
        MaxHealth = maxHealth;
        Health = MaxHealth;
    }
}