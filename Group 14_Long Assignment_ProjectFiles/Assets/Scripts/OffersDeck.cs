using UnityEngine;

public class OffersDeck : MonoBehaviour
{
    public OfferCard[] offerCards;
    public Transform[] playerHands; // Array of player hand transforms
    private int currentPlayer = 0; // Index of the current player

    public void SelectCardAndMoveToPlayer(OfferCard selectedCard)
    {
        // Check if the selected card is in the deck
        if (IsCardInDeck(selectedCard))
        {
            // Move the selected card to the current player's hand
            MoveCardToPlayer(selectedCard, currentPlayer);

            // Increment the current player index for the next turn
            currentPlayer = (currentPlayer + 1) % playerHands.Length;

            // Replace the taken card with a new one
            ReplaceTakenCard(selectedCard);
        }
    }

    private bool IsCardInDeck(OfferCard card)
    {
        foreach (OfferCard deckCard in offerCards)
        {
            if (deckCard == card)
            {
                return true;
            }
        }
        return false;
    }

    private void MoveCardToPlayer(OfferCard card, int playerIndex)
    {
        // Move the card to the specified player's hand by changing its parent transform
        card.transform.SetParent(playerHands[playerIndex]);

        // Perform any additional actions or adjustments you want for the card
        // (e.g., remove it from the deck, update UI, etc.)
    }

    private void ReplaceTakenCard(OfferCard takenCard)
    {
        // Find an available card to replace the taken card
        OfferCard replacementCard = FindAvailableCard();

        if (replacementCard != null)
        {
            // Replace the taken card with the replacement card
            takenCard.cardSprite = replacementCard.cardSprite;

            // Update the visual representation of the taken card (e.g., change sprite)
            // You may need to reference the script/component responsible for the visual representation of the card.
        }
    }

    private OfferCard FindAvailableCard()
    {
        foreach (OfferCard card in offerCards)
        {
            // Check if the card is not currently assigned to any player
            if (card.transform.parent == transform)
            {
                return card;
            }
        }
        return null;
    }
}