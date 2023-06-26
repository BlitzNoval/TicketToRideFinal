using UnityEngine;

[CreateAssetMenu(fileName = "New Destination", menuName = "Ticket to Ride/Destination")]
public class DestinationTicket : ScriptableObject
{
    public string startCity;
    public string endCity;
    public int pointValue;
    public Sprite ticketSprite; // New field for the destination ticket sprite

    public string GetDestinationText()
    {
        // Create a formatted string representing the destination
        return startCity + " - " + endCity;
    }

    public int GetPoints()
    {
        // Return the point value of the destination ticket
        return pointValue;
    }
}
