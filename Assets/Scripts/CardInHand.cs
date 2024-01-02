using TMPro;
using UnityEngine;

public class CardInHand : MonoBehaviour {
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text costText;
    
    public int PlayerIndex { get; private set; }
    public int Attack => CardObject.Attack;
    public int MaxHealth => CardObject.MaxHealth;
    public int Cost => CardObject.Cost;
    
    public Card CardObject { get; private set; }

    public static CardInHand CreateCard(Card cardObject, int playerIndex) {
        var card = Instantiate(Resources.Load<CardInHand>("CardPrefabs/CardInHand"));
        card.PlayerIndex = playerIndex;
        card.CardObject = cardObject;
        return card;
    }
    
    private void Awake() {
        if (CardObject == null) {
            CardObject = new Card(Resources.Load<CardData>("CardData/default"));
        }
        
        costText.text = Cost.ToString();
        attackText.text = Attack.ToString();
        healthText.text = MaxHealth.ToString();
    }
}