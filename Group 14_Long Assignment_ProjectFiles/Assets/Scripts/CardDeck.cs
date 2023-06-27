using UnityEngine;
using System.Collections.Generic;

public class CardDeck : MonoBehaviour
{
    public List<Card> deck;
    public Card[] coloredCards;

    public int CardCount => deck.Count;

    public int cardsPerColor = 12; // Specify the number of train cards per color

    private void Start()
    {
        InitializeDeck();
        ShuffleDeck();

        Debug.Log("Deck Initialized. Card Count: " + CardCount);
    }

    private void InitializeDeck()
    {
        deck.Clear();

        for (int i = 0; i < 14; i++)
        {
            deck.Add(CreateCard());
        }

        foreach (Card coloredCard in coloredCards)
        {
            if (coloredCard != null && coloredCard.cardSprite != null) // Check for the presence of a sprite
            {
                for (int i = 0; i < cardsPerColor; i++)
                {
                    Card duplicatedCard = Instantiate(coloredCard);
                    duplicatedCard.cardSprite = coloredCard.cardSprite; // Assign the card sprite
                    deck.Add(duplicatedCard);
                }
            }
            else
            {
                Debug.LogWarning("Colored card is missing a sprite. Skipping.");
            }
        }

        Debug.Log("Deck Initialized. Card Count: " + CardCount);
    }

    private Card CreateCard()
    {
        Card card = ScriptableObject.CreateInstance<Card>();
        return card;
    }

    private void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int randomIndex = Random.Range(i, deck.Count);
            Card temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }

        Debug.Log("Deck Shuffled. Card Count: " + CardCount);
    }

    public List<Card> DealCards(int numCards)
    {
        List<Card> dealtCards = new List<Card>();

        for (int i = 0; i < numCards; i++)
        {
            if (deck.Count > 0)
            {
                Card card = deck[0];
                deck.RemoveAt(0);
                dealtCards.Add(card);
            }
            else
            {
                Debug.LogWarning("No more cards in the deck!");
                break;
            }
        }

        Debug.Log("Cards Dealt. Card Count: " + CardCount);

        return dealtCards;
    }

    public Card DrawCard()
    {
        if (deck.Count > 0)
        {
            Card card = deck[0];
            deck.RemoveAt(0);
            return card;
        }

        return null;
    }

    public bool HasCards()
    {
        return deck.Count > 0;
    }

    public void ReshuffleDeck()
    {
        InitializeDeck();
        ShuffleDeck();

        Debug.Log("Deck Reshuffled. Card Count: " + CardCount);
    }
}
