using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FetchPlayerName : MonoBehaviour
{
    public TMP_Text player1NameText;
    public TMP_Text player2NameText;

    void Start()
    {
        // Retrieve the player names from PlayerPrefs
        string player1Name = PlayerPrefs.GetString("Player1Name");
        string player2Name = PlayerPrefs.GetString("Player2Name");

        // Display the player names in the game scene
        player1NameText.text = player1Name;
        player2NameText.text = player2Name;
    }
}