public static class CardFactory {
    public static CardOnField CreateCard(CardData data, int playerIndex) {
        return CardOnField.CreateCard(new Card(data), playerIndex);
    }
}