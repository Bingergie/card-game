using System;

public class TurnManager : Singleton<TurnManager> {
    public EventHandler<int> OnTurnStart;
    public EventHandler<int> OnTurnEnd;
    public int CurrentPlayerIndex { get; private set; }
    public int OtherPlayerIndex => 1 - CurrentPlayerIndex;

    public void Start() {
        CurrentPlayerIndex = 0;
    }

    public void EndTurn() {
        OnTurnEnd?.Invoke(this, CurrentPlayerIndex);
        CurrentPlayerIndex = OtherPlayerIndex;
        OnTurnStart?.Invoke(this, CurrentPlayerIndex);
    }
}