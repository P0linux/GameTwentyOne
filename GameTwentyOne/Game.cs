using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Game
    {
        private List<Card> dealerHand;
        private List<Card> playerHand;

        private int dealerScore => dealerHand.Sum(card => (int)card.rank);
        private int playerScore => playerHand.Sum(card => (int)card.rank);

        public Game()
        {
            Deck.Init();
            
            dealerHand = new List<Card>();
            playerHand = new List<Card>();
        }

        public void InitGame()
        {
            var dealerCard = Deck.GetCard();
            var playerCard = Deck.GetCard();

            dealerHand.Add(dealerCard);
            playerHand.Add(playerCard);

            Server.Send($"Your card: {playerCard} Your score: {playerScore} " +
                $"\n Dealer card: {dealerCard} Dealer score: {dealerScore} \n Do you want one more card?");
        }

        public void ProcessMessage(string message)
        {
            switch (message)
            {
                case "Yes":
                    DealPlayerCard();
                    break;
                case "No":
                    DealDealerCard();
                    break;
                default:
                    throw new ArgumentException(message);
            }
        }

        public void DealPlayerCard()
        {
            var card = Deck.GetCard();
            playerHand.Add(card);

            if (playerScore == 21)
            {
                Server.Send($"Your score: {playerScore} \n Dealer score: {dealerScore} \n You win!");
                Server.Stop();
            }
            else if (playerScore > 21)
            {
                Server.Send($"Your score: {playerScore} \n Dealer score: {dealerScore} \n Dealer win!");
                Server.Stop();
            }
            else 
                Server.Send($"Your card: {card} \n\t Your score: {playerScore} \n Do you want one more card?");
        }

        public void DealDealerCard()
        {
            while (dealerScore < 16)
            {
                var card = Deck.GetCard();
                dealerHand.Add(card);
            }

            if (dealerScore > playerScore)
            {
                Server.Send($"Your score: {playerScore} \n Dealer score: {dealerScore} \n Dealer win!");
                Server.Stop();
            }
            else
            {
                Server.Send($"Your score: {playerScore} \n Dealer score: {dealerScore} \n You win!");
                Server.Stop();
            }
        }
    }
}
