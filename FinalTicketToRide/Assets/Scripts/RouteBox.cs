using UnityEngine;

public class RouteBox : MonoBehaviour
{
    public CardColor requiredColor; // The color required to claim this route
    public int requiredColorCount; // The number of color cards required
    public Route route; // Reference to the Route script associated with this route box

    private void OnMouseDown()
    {
        // Check if the player has enough color cards to claim the route
        if (Player.Instance.HasColorCards(requiredColor.ToString(), requiredColorCount))
        {
            // Claim the route
            Player.Instance.ClaimRoute(route);

            // Update the UI and game logic accordingly

            // Optionally, you can add visual feedback for successful route claiming
        }
        else
        {
            Debug.Log("Not enough color cards to claim the route.");
        }
    }
}