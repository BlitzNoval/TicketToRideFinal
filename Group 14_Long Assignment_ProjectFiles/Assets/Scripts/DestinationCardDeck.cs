using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationCardDeck : MonoBehaviour
{
    public List<DestinationTicket> destinationTickets = new List<DestinationTicket>(); // List to hold the destination tickets

    private int currentIndex = 0; // Current index of the ticket being drawn

    // Method to initialize the deck with a list of destination tickets
    public void InitializeDeck(List<DestinationTicket> tickets)
    {
        destinationTickets = tickets;
        ShuffleDeck(); // Shuffle the deck when initializing
    }

    // Method to shuffle the destination ticket deck
    public void ShuffleDeck()
    {
        // Fisher-Yates shuffle algorithm
        int n = destinationTickets.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            DestinationTicket temp = destinationTickets[k];
            destinationTickets[k] = destinationTickets[n];
            destinationTickets[n] = temp;
        }
    }

    // Method to draw a destination ticket from the deck
    public DestinationTicket DrawTicket()
    {
        if (currentIndex >= destinationTickets.Count)
        {
            Debug.LogWarning("No more destination tickets in the deck!");
            return null;
        }

        DestinationTicket ticket = destinationTickets[currentIndex];
        currentIndex++;
        return ticket;
    }
}
