
namespace Server
{
    public enum CardRank
    {
        Six = 6,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack = 2,
        Queen = 3,
        King = 4,
        Ace = 11
    }

    public enum CardSuit
    {
        Clubs,
        Spades,
        Diamonds,
        Hearts
    }

    public readonly record struct Card(CardRank rank, CardSuit suit);
}
