using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Player player;
    public GameObject cardPrefab;
    public GridLayoutGroup gridLayout;
    public ScrollRect scrollRect;

    private void Start()
    {
        if (player != null)
        {
            ShowPlayerHand();
        }
        else
        {
            Debug.LogError("Player reference is missing in PlayerUI!");
        }
    }

    private void ShowPlayerHand()
    {
        // Clear the existing cards from the hand panel
        foreach (Transform child in gridLayout.transform)
        {
            Destroy(child.gameObject);
        }

        // Instantiate card UI objects for each card in the player's hand
        foreach (Card card in player.hand)
        {
            // Create a new GameObject for the card UI object
            GameObject cardObject = Instantiate(cardPrefab);

            // Set the parent of the card object to the hand panel
            cardObject.transform.SetParent(gridLayout.transform);

            // Get the Image component of the card object
            Image cardImage = cardObject.GetComponent<Image>();

            // Set the card sprite
            cardImage.sprite = card.cardSprite;
        }

        // Update the grid layout and scroll view
        gridLayout.constraintCount = player.hand.Count;
        LayoutRebuilder.ForceRebuildLayoutImmediate(gridLayout.GetComponent<RectTransform>());
        scrollRect.verticalNormalizedPosition = 1f;
    }
}
