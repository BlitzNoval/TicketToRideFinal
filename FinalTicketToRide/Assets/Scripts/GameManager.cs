using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public enum PlayerTurn
    {
        Player1,
        Player2
    }

    public PlayerTurn currentPlayer;
    public TMP_Text turnText;
    public GameObject confirmationPanel;
    public TMP_Text confirmationMessage;
    public Button doneButton;
    public Button yesButton;
    public Button noButton;
    public TMP_Text player1NameText;
    public TMP_Text player2NameText;

    private bool isTurnSwitching;
    private CardDeck cardDeck;
    private Player player1;
    private Player player2;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of GameManager found!");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentPlayer = PlayerTurn.Player1; // Set Player 1 as the starting player
        isTurnSwitching = false;
        UpdateTurnUI();

        doneButton.onClick.AddListener(ShowConfirmationPanel);
        yesButton.onClick.AddListener(ConfirmTurnSwitch);
        noButton.onClick.AddListener(CloseConfirmationPanel);

        // Retrieve player names from text fields and assign them
        PlayerData.player1Name = player1NameText.text;
        PlayerData.player2Name = player2NameText.text;
    }

    public CardDeck CardDeck
    {
        get { return cardDeck; }
        set { cardDeck = value; }
    }

    public Player Player1
    {
        get { return player1; }
        set { player1 = value; }
    }

    public Player Player2
    {
        get { return player2; }
        set { player2 = value; }
    }

    public void ClaimRoute(Route route, Player claimingPlayer)
    {
        if (route.IsClaimed)
        {
            Debug.LogWarning("Route has already been claimed!");
            return;
        }

        // Check if the claiming player has enough cards to claim the route
        List<Card> cardsNeeded = route.GetCardsNeeded();
        if (!claimingPlayer.HasCards(cardsNeeded))
        {
            Debug.LogWarning("Player does not have enough cards to claim the route!");
            return;
        }

        // Deduct the required cards from the claiming player's hand
        claimingPlayer.RemoveCards(cardsNeeded);

        // Update the route's claim status and assign the claiming player
        route.Claim(claimingPlayer);

        // Perform any other necessary actions or logic related to claiming the route

        // Example: Update the UI to reflect the claimed route
        route.UpdateUI();
    }

    public bool CanClaimRoute(Route route, Player claimingPlayer)
    {
        // Add your custom conditions here to determine if the player can claim the route
        // For example, checking if the player has enough cards, if the route is already claimed, etc.

        // Check if the route has already been claimed
        if (route.IsClaimed)
        {
            return false;
        }

        // Check if the claiming player has enough cards to claim the route
        List<Card> cardsNeeded = route.GetCardsNeeded();
        return claimingPlayer.HasCards(cardsNeeded);
    }

    bool IsGameOver()
    {
        // Add your implementation here
        return false; // Replace false with your game-over condition logic
    }

    void GameOver()
    {
        // Add your game-over actions and logic here
        // For example, displaying a game-over screen, resetting the game, etc.
    }

    public Player GetActivePlayer()
    {
        // Return the corresponding player based on the current turn
        if (currentPlayer == PlayerTurn.Player1)
        {
            return Player1;
        }
        else
        {
            return Player2;
        }
    }

    public void SwitchTurn()
    {
        if (isTurnSwitching)
            return;

        isTurnSwitching = true;
        StartCoroutine(SwitchTurnCoroutine());
    }

    private System.Collections.IEnumerator SwitchTurnCoroutine()
    {
        // Implement any necessary actions or logic before confirming the turn switch

        // Display a confirmation message or UI
        confirmationMessage.text = "Confirm";
        confirmationPanel.SetActive(true);

        // Wait for player confirmation (e.g., clicking a button)
        yield return new WaitUntil(() => !confirmationPanel.activeSelf);

        // Only switch the turn if the confirmation flag is true
        if (isTurnSwitching)
        {
            // Switch the current player
            if (currentPlayer == PlayerTurn.Player1)
                currentPlayer = PlayerTurn.Player2;
            else
                currentPlayer = PlayerTurn.Player1;

            // Call any necessary actions or methods at the end of a turn
            EndTurnActions();

            // Update the turn UI
            UpdateTurnUI();
        }

        isTurnSwitching = false;
    }

    private void EndTurnActions()
    {
        // Implement any necessary actions or logic that need to be executed at the end of a turn
        // For example, you can update the UI, check for game-over conditions, etc.
    }

    private void UpdateTurnUI()
    {
        if (turnText != null)
        {
            string turnString = (currentPlayer == PlayerTurn.Player1) ? PlayerData.player1Name + "'s Turn" : PlayerData.player2Name + "'s Turn";
            turnText.text = turnString;
        }
    }

    private void ShowConfirmationPanel()
    {
        confirmationPanel.SetActive(true);
        yesButton.onClick.AddListener(ConfirmTurnSwitch);
        noButton.onClick.AddListener(CloseConfirmationPanel);
    }

    private void ConfirmTurnSwitch()
    {
        confirmationPanel.SetActive(false);
        SwitchTurn();
    }

    private void CloseConfirmationPanel()
    {
        confirmationPanel.SetActive(false);
        isTurnSwitching = false;
    }
}
