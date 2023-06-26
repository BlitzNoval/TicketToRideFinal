using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FetchPlayerName : MonoBehaviour
{
    public TMP_Text player1NameText;
    public TMP_Text player2NameText;

    void Start()
    {
        player1NameText.text = PlayerData.player1Name;
        player2NameText.text = PlayerData.player2Name;
    }
}