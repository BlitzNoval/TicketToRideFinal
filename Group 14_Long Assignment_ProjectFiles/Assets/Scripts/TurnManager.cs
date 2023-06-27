using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public Button[] player1Buttons;
    public Button[] player2Buttons;

    public Player[] players;
    private int currentPlayerIndex = 0;
    private bool isTurnInProgress = true;

    private void Start()
    {
        StartTurn();
    }

    public void StartTurn()
    {
        if (isTurnInProgress)
        {
            Debug.LogWarning("A turn is already in progress!");
            return;
        }

        isTurnInProgress = true;

        // Enable the UI buttons for the current player's turn
        Player currentPlayer = players[currentPlayerIndex];
        EnableButtonsForPlayer(currentPlayer);

        // Disable UI buttons for the other player
        Player otherPlayer = GetOtherPlayer();
        DisableButtonsForPlayer(otherPlayer);
    }



    private void EnableButtonsForPlayer(Player player)
    {
        foreach (Button button in player.buttons)
        {
            button.interactable = true;
        }
    }

    private void DisableButtonsForPlayer(Player player)
    {
        foreach (Button button in player.buttons)
        {
            button.interactable = true;
        }
    }

    private Player GetOtherPlayer()
    {
        int otherPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        return players[otherPlayerIndex];
    }
}


