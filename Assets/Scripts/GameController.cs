using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one GameController in scene!");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void HandleAttack(CardOnField attacker, CardOnField defender) {
        attacker.TakeDamage(defender.GetAttack());
        defender.TakeDamage(attacker.GetAttack());
    }
}