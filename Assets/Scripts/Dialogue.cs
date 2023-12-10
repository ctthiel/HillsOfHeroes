using System.Collections;
using UnityEngine;
using TMPro;
public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public Canvas canvas;
    public string[] lines;
    public float textSpeed;

    private bool dialogueTriggered = false; 
    private int index;

    void Start()
    {
        if (textComponent == null)
        {
            enabled = false;
            return;
        }
        textComponent.text = string.Empty;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && dialogueTriggered == true)
        {
            if (textComponent.text == lines[index]) // Check if current text is fully displayed
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines(); // Stop coroutine and display full line
                textComponent.text = lines[index];
            }
        }
    }   
    
    // Starts dialogue when player enters trigger zone
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Trigger Zone");

        PlayerCombat playerCombat = other.GetComponent<PlayerCombat>();
        if (!dialogueTriggered && other.CompareTag("Player"))
        {
            dialogueTriggered = true;
            Debug.Log("Triggering Dialogue");

            if (canvas != null)
            {
                canvas.gameObject.SetActive(true);
                StartDialogue();
            }
        }
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    // Graduallty types the dialogue lines to look cool
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    // Moves to next sentence
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            if (canvas != null)
            {
                canvas.gameObject.SetActive(false);
            }
            
        }
    }
}
