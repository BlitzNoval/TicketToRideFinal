using UnityEngine;
using UnityEngine.UI;

public class OfferCard : MonoBehaviour
{
    public Sprite cardSprite;
    public bool isSelected = false;

    private Image cardImage;

    private void Awake()
    {
        cardImage = GetComponent<Image>();
    }

    public void SelectCard()
    {
        isSelected = !isSelected;

        // Change the appearance of the card based on selection status
        if (isSelected)
        {
            // Apply some visual indication, e.g., change color or outline
            cardImage.color = Color.green;

            // Move the selected card to the player's hand
            // Player.Instance.AddToHand(this); // Remove this line
        }
        else
        {
            // Reset the visual indication
            cardImage.color = Color.white;
        }
    }
}
