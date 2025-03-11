using UnityEngine;
using DialogueEditor;
using System.Collections;
using UnityEngine.SceneManagement;

public class CutSceneConv : MonoBehaviour
{
    [SerializeField] private NPCConversation conversation1;
    [SerializeField] private NPCConversation conversation2;
    public float waitTime = 5f; // Time to wait between dialogues

    void Start()
    {
        StartCoroutine(StartDialogueSequence());
    }

    IEnumerator StartDialogueSequence()
    {
        // Start dialogue with conversation1
        if (conversation1 != null)
        {
            ConversationManager.Instance.StartConversation(conversation1);
            Debug.Log("Started dialogue with Conv1.");
        }
        else
        {
            Debug.LogWarning("Conversation1 is not assigned.");
        }

        // Wait for the specified duration
        yield return new WaitForSeconds(waitTime - 1);

        // End the first conversation
        ConversationManager.Instance.EndConversation();

        // Start dialogue with conversation2
        if (conversation2 != null)
        {
            ConversationManager.Instance.StartConversation(conversation2);
            Debug.Log("Started dialogue with Conv2.");
        }
        else
        {
            Debug.LogWarning("Conversation2 is not assigned.");
        }

        // Wait for the specified duration
        yield return new WaitForSeconds(waitTime + 1);

        // End the second conversation
        ConversationManager.Instance.EndConversation();

        // Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Loaded next scene.");
    }
}
