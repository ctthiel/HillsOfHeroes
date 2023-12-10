using UnityEngine;

public class Menu : MonoBehaviour
{
    public Canvas startMenuCanvas;
    public GameObject uiElements;

    public void PlayButton()
    {
        startMenuCanvas.gameObject.SetActive(false);
        uiElements.SetActive(true);
    }

    public void QuitButton()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
