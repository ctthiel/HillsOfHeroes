using UnityEngine;
using TMPro;

public class ChestController : MonoBehaviour
{
    public int totalChests = 5;
    public TextMeshProUGUI chestCountText;
    public AudioClip chestSound;
    public AudioSource chestSource;
    public AudioClip victorySound;
    public AudioSource victorySource;
    public Canvas victoryCanvas;

    // Static to keep track of collected chests across all instances
    private static int collectedChests = 0; 

    void Start()
    {
        UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectChest();
        }
    }

    void CollectChest()
    {
        if (collectedChests < totalChests)
        {
            collectedChests++;           
            chestSource.PlayOneShot(chestSound);

            // Makes chest disapear until the audio finishes, then destroyed
            // (I'm sure there was a better way to do this)
            transform.localScale = Vector3.zero; 

            UpdateUI();

            if (collectedChests == totalChests) // After collecting all chests display victory text
            {
                Debug.Log("Victory! Ending logic here.");
                Invoke("EnableVictoryCanvas", 3.0f);
            }

            // Waits until audio finished to destroy chest
            Destroy(gameObject, chestSound.length);
        }
    }

    void EnableVictoryCanvas()
    {
        victoryCanvas.gameObject.SetActive(true);
        victorySource.PlayOneShot(victorySound);
    }

    // Updates collected chests UI
    void UpdateUI()
    {
        if (chestCountText != null)
        {
            chestCountText.text = "Chests: " + collectedChests + " / " + totalChests;
        }
    }
}
