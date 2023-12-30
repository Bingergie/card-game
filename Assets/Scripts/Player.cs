using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public List<CardStats> deck;
    
    public void Attack(Card attacker,Card target) {
        attacker.Attack(target);
    }
}