using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToRules : MonoBehaviour
{
    public void LoadRulesScene()
    {
        SceneManager.LoadScene("Rules");
    }
}