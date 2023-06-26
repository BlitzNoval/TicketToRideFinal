using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public List<Card> hand;
    public Button[] buttons;
    public Button claimRouteButton;
    public Button drawTrainCardButton;
    public Button drawDestinationTicketButton;
    public GameObject cardPrefab; // Reference to the card prefab
    public GridLayoutGroup gridLayout; // Reference to the grid layout component
    public ScrollRect scrollRect; // Reference to the scroll rect component
    public Sprite locomotiveSprite; // Reference to the locomotive sprite
    public Dictionary<string, Sprite> coloredCardSprites; // Dictionary to hold colored card sprites
    public CardDeck cardDeck;
    public DestinationCardDeck destinationCardDeck; // Reference to the destination card deck

    // Placeholder sprite variables for different colors
    public Sprite redSprite;
    public Sprite blueSprite;
    public Sprite greenSprite;
    public Sprite pinkSprite; // Player 2 card color
    public Sprite orangeSprite;
    public Sprite whiteSprite;
    public Sprite yellowSprite;
    public Sprite blackSprite;
    public Sprite locomotive;

    private int maxHandSize = 30; // Maximum number of cards a player can hold
    public List<DestinationTicket> destinationCardHand;
    public int maxDestinationCardHandSize = 10;

    public GameObject destinationCardPrefab; // Reference to the destination card prefab
    public GameObject destinationCardHandPanel; // Reference to the destination card hand panel
    public GridLayoutGroup destinationCardHandGridLayout; // Reference to the grid layout of destination card hand panel
    public ScrollRect destinationCardHandScrollRect; // Reference to the scroll rect of destination card hand panel

    private void Start()
    {
        hand = new List<Card>(); // Initialize the hand list
        destinationCardHand = new List<DestinationTicket>();

        if (drawTrainCardButton != null)
        {
            drawTrainCardButton.onClick.AddListener(DrawCard);
        }

        // Initialize the coloredCardSprites dictionary
        coloredCardSprites = new Dictionary<string, Sprite>();

        // Add color sprites to the dictionary
        coloredCardSprites.Add("Red", redSprite);
        coloredCardSprites.Add("Blue", blueSprite);
        coloredCardSprites.Add("Green", greenSprite);
        coloredCardSprites.Add("Pink", pinkSprite); // Player 2 card color
        coloredCardSprites.Add("Orange", orangeSprite);
        coloredCardSprites.Add("White", whiteSprite);
        coloredCardSprites.Add("Yellow", yellowSprite);
        coloredCardSprites.Add("Black", blackSprite);
        coloredCardSprites.Add("Locomotive", locomotiveSprite);

        DealInitialCards(maxHandSize);
        ShowCards();
    }

    public void AddToHand(Card card)
    {
        if (hand.Count < maxHandSize)
        {
            hand.Add(card);
            ShowCards();
        }
        else
        {
            Debug.LogWarning("Hand is full. Cannot add more cards.");
        }
    }

    public bool CanDrawCard()
    {
        if (hand.Count < maxHandSize)
        {
            return true;
        }
        return false;
    }

    private void DrawCard()
    {
        if (CanDrawCard())
        {
            // Implement your logic to draw a card from a deck
            Card drawnCard = cardDeck.DrawCard();
            if (drawnCard != null)
            {
                AddToHand(drawnCard);
            }
            else
            {
                Debug.LogWarning("No more cards in the deck!");
            }

            // Increase the max hand size by 1
            IncrementMaxHandSize();

            // Update the draw card button interactivity
            UpdateDrawCardButtonInteractivity();
        }
        else
        {
            Debug.LogWarning("Hand is full. Cannot draw more cards.");
        }
    }

    public void IncrementMaxHandSize()
    {
        maxHandSize++;
    }

    private void UpdateDrawCardButtonInteractivity()
    {
        if (drawTrainCardButton != null)
        {
            drawTrainCardButton.interactable = CanDrawCard();
        }
    }

    public bool HasCards(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            if (!hand.Contains(card))
            {
                return false;
            }
        }
        return true;
    }

    public bool RemoveCards(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            if (!hand.Remove(card))
            {
                return false;
            }
        }
        return true;
    }

    private void DealInitialCards(int numCards)
    {
        List<Card> dealtCards = cardDeck.DealCards(numCards);
        foreach (Card card in dealtCards)
        {
            AddToHand(card);
        }
    }

    public void ShowCards()
    {
        // Clear the existing cards from the hand panel
        foreach (Transform child in gridLayout.transform)
        {
            Destroy(child.gameObject);
        }

        // Instantiate card UI objects for each card in the hand
        foreach (Card card in hand)
        {
            // Create a new GameObject for the card UI object
            GameObject cardObject = Instantiate(cardPrefab);

            // Set the parent of the card object to the hand panel
            cardObject.transform.SetParent(gridLayout.transform);

            // Get the Image component of the card object
            Image cardImage = cardObject.GetComponent<Image>();

            // Set the card sprite based on the card's color
            string color = card.Color;
            if (coloredCardSprites.ContainsKey(color))
            {
                cardImage.sprite = coloredCardSprites[color];
            }
            else
            {
                Debug.LogWarning("Missing sprite for card color: " + color);
            }
        }

        // Update the grid layout and scroll view
        gridLayout.constraintCount = hand.Count;
        LayoutRebuilder.ForceRebuildLayoutImmediate(gridLayout.GetComponent<RectTransform>());
        scrollRect.verticalNormalizedPosition = 1f;
    }

    public void AddDestinationCard(DestinationTicket destinationTicket)
    {
        // Check if the destination card hand is already full
        if (destinationCardHand.Count >= maxDestinationCardHandSize)
        {
            Debug.LogWarning("Destination card hand is full. Cannot add more cards.");
            return;
        }

        // Add the destination card to the destination card hand
        destinationCardHand.Add(destinationTicket);

        // Optionally, you can perform additional logic or validation here based on your game's rules

        // Update the UI to show the added destination card
        ShowDestinationCardHand();
    }

    public void ShowDestinationCardHand()
    {
        // Clear the existing cards from the destination card hand panel
        foreach (Transform child in destinationCardHandPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Instantiate card UI objects for each destination ticket in the destination card hand
        foreach (DestinationTicket destinationTicket in destinationCardHand)
        {
            // Create a new GameObject for the destination card UI object
            GameObject destinationCardObject = Instantiate(destinationCardPrefab);

            // Set the parent of the destination card object to the destination card hand panel
            destinationCardObject.transform.SetParent(destinationCardHandPanel.transform);

            // Get the DestinationCardUI component of the destination card object
            DCardUI dCardUI = destinationCardObject.GetComponent<DCardUI>();

            // Set the destination card's data in the DestinationCardUI component
            dCardUI.SetDestinationTicket(destinationTicket);
        }

        // Update the grid layout and scroll view of the destination card hand
        destinationCardHandGridLayout.constraintCount = destinationCardHand.Count;
        LayoutRebuilder.ForceRebuildLayoutImmediate(destinationCardHandGridLayout.GetComponent<RectTransform>());
        destinationCardHandScrollRect.verticalNormalizedPosition = 1f;
    }

    public bool HasDestinationCard(DestinationTicket destinationTicket)
    {
        return destinationCardHand.Contains(destinationTicket);
    }

    public bool HasColorCards(int numCards, string color)
    {
        int count = 0;
        foreach (Card card in hand)
        {
            if (card.Color == color || card.Color == "Locomotive")
            {
                count++;
            }
        }
        return count >= numCards;
    }

    public void RemoveColorCards(int numCards, string color)
    {
        int count = 0;
        List<Card> cardsToRemove = new List<Card>();
        foreach (Card card in hand)
        {
            if (card.Color == color || card.Color == "Locomotive")
            {
                cardsToRemove.Add(card);
                count++;
                if (count == numCards)
                {
                    break;
                }
            }
        }
        foreach (Card card in cardsToRemove)
        {
            hand.Remove(card);
        }
    }

    public List<DestinationTicket> GetAvailableDestinations()
    {
        List<DestinationTicket> availableDestinations = new List<DestinationTicket>();
        foreach (DestinationTicket destinationTicket in destinationCardDeck.destinationTickets)
        {
            if (!destinationCardHand.Contains(destinationTicket))
            {
                availableDestinations.Add(destinationTicket);
            }
        }
        return availableDestinations;
    }
}