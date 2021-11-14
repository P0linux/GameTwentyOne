using System;
using System.Collections.Generic;

namespace Server
{
    public static class Deck
    {
        public static List<Card> Cards { get; set; }

        public static void Init()
        {
            SetDeck();
            Shuffle();
        }

        public static void Shuffle()
        {
            Random rng = new Random();

            int n = Cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card card = Cards[k];
                Cards[k] = Cards[n];
                Cards[n] = card;
            }
        }

        public static Card GetCard()
        {
            Card card = Cards[0];

            Cards.RemoveAt(0);

            return card;
        }

        private static void SetDeck()
        {
            Cards = new List<Card>();

            for (int i = 2; i <= 11; i++)
            {
                if (i == 5) continue;

                for (int j = 0; j < 4; j++)
                {
                    Cards.Add(new Card((CardRank)i, (CardSuit)j));
                }
            }
        }
    }
}
