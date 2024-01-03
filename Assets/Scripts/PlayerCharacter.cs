using TMPro;
using UnityEngine;

[RequireComponent(typeof(DamageableEntity))]
public class PlayerCharacter : MonoBehaviour {
    public const int MaxHealth = 20;

    [SerializeField] private TMP_Text healthText;
    
    public int PlayerIndex { get; private set; }
    public int Health => _damageableEntity.Health;
    
    private DamageableEntity _damageableEntity;
    
    public static PlayerCharacter CreateCharacter(int playerIndex) {
        var character = Instantiate(Resources.Load<PlayerCharacter>("CardPrefabs/PlayerCharacter"));
        character.PlayerIndex = playerIndex;
        return character;
    }
    
    private void Awake() {
        _damageableEntity = GetComponent<DamageableEntity>();
        _damageableEntity.SetHealthToMax(MaxHealth);
        UpdateText();
    }
    
    public void TakeDamage(int damage) {
        _damageableEntity.TakeDamage(damage);
        UpdateText();
        if (Health <= 0) {
            GameController.Instance.OnPlayerWin?.Invoke(this, 1 - PlayerIndex); // sets winner to other player
        }
    }
    
    private void UpdateText() {
        healthText.text = Health.ToString();
    }
}