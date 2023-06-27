using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField player1NameInput; // Reference to player 1's input field
    public TMP_InputField player2NameInput; // Reference to player 2's input field
    public TextMeshProUGUI errorText; // Reference to the error text component
    public float errorDisplayDuration = 3f; // Duration for which the error text is displayed

    public void PlayGame()
    {
        if (string.IsNullOrEmpty(player1NameInput.text) || string.IsNullOrEmpty(player2NameInput.text))
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "Please enter names for both players!";
            Invoke("HideErrorText", errorDisplayDuration);
            return;
        }

        // Get the custom player names from the input fields
          PlayerPrefs.SetString("Player1Name", player1NameInput.text);
        PlayerPrefs.SetString("Player2Name", player2NameInput.text);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void HideErrorText()
    {
        errorText.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Successfully Quit Game");
        Application.Quit();
    }
}