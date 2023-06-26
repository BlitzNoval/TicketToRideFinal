using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationCardDeck : MonoBehaviour
{
    public List<DestinationTicket> destinationCards = new List<DestinationTicket>(); // List to hold the destination cards

    private int currentIndex = 0; // Current index of the card being drawn

    // Method to initialize the deck with a list of destination cards
    public void InitializeDeck(List<DestinationTicket> cards)
    {
        destinationCards = cards;
        ShuffleDeck(); // Shuffle the deck when initializing
    }

    // Method to shuffle the destination card deck
    public void ShuffleDeck()
    {
        // Fisher-Yates shuffle algorithm
        int n = destinationCards.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            DestinationTicket temp = destinationCards[k];
            destinationCards[k] = destinationCards[n];
            destinationCards[n] = temp;
        }
    }

    // Method to draw a destination card from the deck
    public DestinationTicket DrawCard()
    {
        if (currentIndex >= destinationCards.Count)
        {
            Debug.LogWarning("No more destination cards in the deck!");
            return null;
        }

        DestinationTicket card = destinationCards[currentIndex];
        currentIndex++;
        return card;
    }
}
