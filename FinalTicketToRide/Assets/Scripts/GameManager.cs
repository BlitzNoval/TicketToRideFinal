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
     public Dropdown destinationDropdown;
    

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

        destinationDropdown.onValueChanged.AddListener(OnDestinationDropdownValueChanged);
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

    
    {
        // Handle the claim route action
private void OnClaimRouteButtonClicked()
{
    // Handle the claim route action

    // Get the selected destination from the dropdown
    string selectedDestination = destinationDropdown.options[destinationDropdown.value].text;

    // Get the active player
    Player activePlayer = GetActivePlayer();

    // Check if the active player has the selected destination in their hand
    if (activePlayer.HasDestinationCard(selectedDestination))
    {
        // Example: Assuming you have the required variables to determine the number of cards and color
        int numCards = 3;
        string color = "Green";
        if (!activePlayer.HasColorCards(numCards, color))
        {
            Debug.LogWarning("Player does not have enough color cards to claim the route!");
            return;
        }

        // Get the required color cards for the selected destination
        List<string> requiredColors = GetRequiredColorsForDestination(selectedDestination);

        // Check if the active player has enough color cards to claim the route
        if (!activePlayer.HasColorCards(requiredColors))
        {
            Debug.LogWarning("Player does not have enough color cards to claim the route!");
            return;
        }

        // Deduct the required color cards from the active player's hand
        activePlayer.RemoveColorCards(requiredColors);

        // Perform the route claiming logic
        // (Call the ClaimRoute method with the selected route and the active player)

        // Other necessary actions and logic...

        // Update the UI and other necessary components
    }
    else
    {
        Debug.LogWarning("Player does not have the selected destination card!");
    }
}
        // Check if the active player has the selected destination in their hand
       public bool HasDestinationCard(string destination)
{
    foreach (DestinationTicket destinationTicket in destinationCardHand)
    {
        if (destinationTicket.Destination == destination)
        {
            return true;
        }
    }
    return false;

    // Example: Assuming you have the required variables to determine the number of cards and color
int numCards = 3;
string color = "Green";
if (!activePlayer.HasColorCards(numCards, color))
{
    Debug.LogWarning("Player does not have enough color cards to claim the route!");
    return;
}

}

        // Get the required color cards for the selected destination
private List<string> GetRequiredColorsForDestination(string destination)
{
    // Implement your logic to determine the required color cards for the given destination
    // This could involve querying your game data or other custom rules

    // For demonstration purposes, let's assume the required colors are fixed for each destination

    // Destination "A" requires 2 red color cards and 1 blue color card
    if (destination == "A")
    {
        return new List<string> { "Red", "Red", "Blue" };
    }
    // Destination "B" requires 3 green color cards and 1 yellow color card
    else if (destination == "B")
    {
        return new List<string> { "Green", "Green", "Green", "Yellow" };
    }
    // Add more cases for other destinations...

    // If the destination is not found, return an empty list
    return new List<string>();
}

        // Deduct the required color cards from the active player's hand
        activePlayer.RemoveColorCards(requiredColors);

        // Perform the route claiming logic
        // (Call the ClaimRoute method with the selected route and the active player)

        // Other necessary actions and logic...

        // Update the UI and other necessary components
    }

    private List<string> GetRequiredColorsForDestination(string destination)
    {
        // Implement your logic to determine the required color cards for the given destination
        // This could involve querying your game data or other custom rules

        // For demonstration purposes, let's assume the required colors are fixed for each destination

        // Destination "A" requires 2 red color cards and 1 blue color card
        if (destination == "A")
        {
            return new List<string> { "Red", "Red", "Blue" };
        }
        // Destination "B" requires 3 green color cards and 1 yellow color card
        else if (destination == "B")
        {
            return new List<string> { "Green", "Green", "Green", "Yellow" };
        }
        // Add more cases for other destinations...

public List<string> GetAvailableDestinations()
{
    List<string> availableDestinations = new List<string>();
    foreach (DestinationTicket destinationTicket in destinationCardDeck.destinationTickets)
    {
        if (!destinationCardHand.Contains(destinationTicket))
        {
            availableDestinations.Add(destinationTicket.Destination);
        }
    }
    return availableDestinations;
}

        // If the destination is not found, return an empty list
        return new List<string>();
    }

    private void OnDestinationDropdownValueChanged(int index)
    {
        // Update the available destination options based on the active player's hand

        // Clear the existing options
        destinationDropdown.ClearOptions();

        // Get the active player
        Player activePlayer = GetActivePlayer();

        // Get the available destinations from the active player's hand
        List<string> availableDestinations = activePlayer.GetAvailableDestinations();

        // Add the available destinations to the dropdown options
        destinationDropdown.AddOptions(availableDestinations);

        // Select the first option by default
        if (availableDestinations.Count > 0)
        {
            destinationDropdown.value = 0;
        }
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
