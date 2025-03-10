using UnityEngine;
using DialogueEditor;

public class ConversationStarter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private NPCConversation conversationName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && (Input.GetKeyDown(KeyCode.E)||Input.GetMouseButtonDown(0)))
        {
            ConversationManager.Instance.StartConversation(conversationName);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && (Input.GetKeyDown(KeyCode.E)||Input.GetMouseButtonDown(0)))
        {
            ConversationManager.Instance.StartConversation(conversationName);
        }
    }
    private void OnTriggerExit(Collider other)
    {if (other.CompareTag("Player"))
        {
            ConversationManager.Instance.EndConversation();
        }
    }
}
