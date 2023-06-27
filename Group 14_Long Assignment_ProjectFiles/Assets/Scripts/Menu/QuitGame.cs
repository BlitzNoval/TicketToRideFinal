using UnityEngine;

using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    // Function to quit the game
    public void Quit()
    {
        Debug.Log("Quitting the game...");

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
   

}
