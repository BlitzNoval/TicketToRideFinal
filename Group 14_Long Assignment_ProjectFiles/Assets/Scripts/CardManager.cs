using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class CardManager : MonoBehaviour
{
    public CardDeck cardDeck;
    public Player player1;
    public Player player2;

    public TextMeshProUGUI player1CardCountText;
    public TextMeshProUGUI player2CardCountText;

    private void Start()
    {
        DealInitialCards();
        UpdatePlayerCardCount();
    }

    private void DealInitialCards()
    {
        List<Card> player1Cards = cardDeck.DealCards(8);
        foreach (Card card in player1Cards)
        {
            player1.AddToHand(card);
        }

        List<Card> player2Cards = cardDeck.DealCards(8);
        foreach (Card card in player2Cards)
        {
            player2.AddToHand(card);
        }
    }

    private void Update()
    {
        UpdateCardCountTexts();
    }

    private void UpdateCardCountTexts()
    {
        player1CardCountText.text = player1.hand.Count.ToString();
        player2CardCountText.text = player2.hand.Count.ToString();
    }

    private void UpdatePlayerCardCount()
    {
        player1CardCountText.text = player1.hand.Count.ToString();
        player2CardCountText.text = player2.hand.Count.ToString();
    }

    public void Player1DrawCard()
    {
        Card card = cardDeck.DrawCard();
        if (card != null)
        {
            player1.AddToHand(card);
            UpdatePlayerCardCount();
        }
        else
        {
            Debug.LogWarning("No more cards in the deck!");
        }
    }

    public void Player2DrawCard()
    {
        Card card = cardDeck.DrawCard();
        if (card != null)
        {
            player2.AddToHand(card);
            UpdatePlayerCardCount();
        }
        else
        {
            Debug.LogWarning("No more cards in the deck!");
        }
    }
}
