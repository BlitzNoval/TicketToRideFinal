using UnityEngine;

[CreateAssetMenu(fileName = "Routes", menuName = "ScriptableObjects/Routes", order = 1)]
public class Routes : ScriptableObject
{
    public string startCity;
    public string endCity;
    public CardColor[] requiredCards;
}