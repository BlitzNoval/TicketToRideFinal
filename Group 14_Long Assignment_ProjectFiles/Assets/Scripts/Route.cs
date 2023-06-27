using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Route : MonoBehaviour
{
    public bool IsClaimed { get; set; }
    public Player ClaimingPlayer { get; set; }
    public string RequiredColor { get; set; }
    public int RequiredColorCount { get; set; }
    public GameObject routeBox;
    public Routes routeData; // Reference to the Routes ScriptableObject
    public DestinationTicket? destinationTicket;
    public Button claimButton;

    private void Start()
    {
        claimButton.onClick.AddListener(ClaimRoute);
    }

    public void Claim(Player claimingPlayer)
    {
        if (IsClaimed)
        {
            Debug.Log("Route is already claimed.");
            return;
        }

        claimingPlayer.ClaimRoute(this);
    }

    public void ClaimRoute()
    {
        if (IsClaimed)
        {
            Debug.Log("Route is already claimed.");
            return;
        }

        if (ClaimingPlayer != null && ClaimingPlayer.HasColorCards(RequiredColor, RequiredColorCount))
        {
            // Add your logic to handle claiming the route here
            Debug.Log("Claiming the route from " + routeData.startCity + " to " + routeData.endCity);
        }
        else
        {
            Debug.Log("Not enough color cards to claim the route.");
        }
    }

    public List<Card> GetCardsNeeded()
    {
        List<Card> cardsNeeded = new List<Card>();

        // Implement the logic to determine the cards needed to claim the route

        return cardsNeeded;
    }

    public void UpdateUI()
    {
        // Implement the logic to update the UI for the claimed route
    }
}
