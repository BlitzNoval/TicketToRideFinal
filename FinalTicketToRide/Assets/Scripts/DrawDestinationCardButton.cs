using UnityEngine;
using UnityEngine.UI;

public class DrawDestinationCardButton : MonoBehaviour
{
    public Button drawDestinationCardButtonPlayer1;
    public Button drawDestinationCardButtonPlayer2;
    public DestinationCardDeck destinationCardDeck;
    public Player player1;
    public Player player2;

    private void Start()
    {
        if (drawDestinationCardButtonPlayer1 != null)
        {
            drawDestinationCardButtonPlayer1.onClick.AddListener(() => DrawDestinationCardForPlayer(player1));
        }

        if (drawDestinationCardButtonPlayer2 != null)
        {
            drawDestinationCardButtonPlayer2.onClick.AddListener(() => DrawDestinationCardForPlayer(player2));
        }
    }

    private void DrawDestinationCardForPlayer(Player player)
    {
        DestinationTicket drawnTicket = destinationCardDeck.DrawCard();
        if (drawnTicket != null)
        {
            player.AddDestinationCard(drawnTicket);
        }
        else
        {
            Debug.LogWarning("No more destination cards in the deck!");
        }

        UpdateDrawDestinationCardButtonInteractivity();
    }

    private void UpdateDrawDestinationCardButtonInteractivity()
    {
        if (drawDestinationCardButtonPlayer1 != null)
        {
            drawDestinationCardButtonPlayer1.interactable = CanDrawDestinationCard(player1);
        }

        if (drawDestinationCardButtonPlayer2 != null)
        {
            drawDestinationCardButtonPlayer2.interactable = CanDrawDestinationCard(player2);
        }
    }

    private bool CanDrawDestinationCard(Player player)
    {
        // Implement your logic to determine if the player can draw a destination card
        // For example, you can check if the player has reached a certain point in the game
        // or if the destination card deck still has cards remaining.
        // Return true if the player can draw a destination card; otherwise, return false.
        // You can customize this based on your game's rules.
        return true;
    }
}
