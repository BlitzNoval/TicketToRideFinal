using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DCardUI : MonoBehaviour
{
    public TMP_Text destinationText;
    public TMP_Text pointsText;

public void SetDestinationTicket(DestinationTicket destinationTicket)
{
    if (destinationTicket != null)
    {
        // Set the destination text
        destinationText.text = destinationTicket.GetDestinationText();

        // Set the points text
        pointsText.text = destinationTicket.GetPoints().ToString();
    }
    else
    {
        Debug.LogWarning("Null destination ticket provided.");
    }
}
}
