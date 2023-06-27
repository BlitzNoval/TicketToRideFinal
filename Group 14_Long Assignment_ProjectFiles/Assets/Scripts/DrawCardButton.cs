using UnityEngine;
using UnityEngine.UI;

public class DrawCardButton : MonoBehaviour
{
    public Button drawCardButtonPlayer1;
    public Button drawCardButtonPlayer2;
    public CardDeck cardDeck;
    public Player player1;
    public Player player2;

    private int maxCardDraws = 2; // Maximum number of card draws allowed per turn
    private int cardDrawCountPlayer1 = 0; // Counter for player 1's card draws
    private int cardDrawCountPlayer2 = 0; // Counter for player 2's card draws

    private void Start()
    {
        if (drawCardButtonPlayer1 != null)
        {
            drawCardButtonPlayer1.onClick.AddListener(() => DrawCardForPlayer(player1));
        }

        if (drawCardButtonPlayer2 != null)
        {
            drawCardButtonPlayer2.onClick.AddListener(() => DrawCardForPlayer(player2));
        }
    }

    private void DrawCardForPlayer(Player player)
    {
        if (CanDrawCard(player))
        {
            Card drawnCard = cardDeck.DrawCard();
            if (drawnCard != null)
            {
                player.AddToHand(drawnCard);
            }
            else
            {
                Debug.LogWarning("No more cards in the deck!");
            }

            player.IncrementMaxHandSize();
            UpdateDrawCardButtonInteractivity();
        }
        else
        {
            Debug.LogWarning("Hand is full or maximum card draws reached. Cannot draw more cards.");
        }
    }

    private bool CanDrawCard(Player player)
    {
        if (player == player1 && cardDrawCountPlayer1 >= maxCardDraws)
        {
            return false;
        }
        else if (player == player2 && cardDrawCountPlayer2 >= maxCardDraws)
        {
            return false;
        }

        return player.CanDrawCard();
    }

    private void UpdateDrawCardButtonInteractivity()
    {
        if (drawCardButtonPlayer1 != null)
        {
            bool canDrawCardPlayer1 = CanDrawCard(player1);
            drawCardButtonPlayer1.interactable = canDrawCardPlayer1;

            if (!canDrawCardPlayer1)
            {
                drawCardButtonPlayer1.gameObject.SetActive(false);
            }
        }

        if (drawCardButtonPlayer2 != null)
        {
            bool canDrawCardPlayer2 = CanDrawCard(player2);
            drawCardButtonPlayer2.interactable = canDrawCardPlayer2;

            if (!canDrawCardPlayer2)
            {
                drawCardButtonPlayer2.gameObject.SetActive(false);
            }
        }
    }
}
