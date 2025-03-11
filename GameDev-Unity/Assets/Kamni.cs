using UnityEngine;

public class Kamni : MonoBehaviour
{
    public float repearingRange = 3f;
    public Transform player;
    private bool isReapearingRange = false;

    void Update()
    {
        GameObject armory = GameObject.FindWithTag("Armory");
        if (armory != null)
        {
            float distanceToPlayer = Vector3.Distance(armory.transform.position, player.position);
            isReapearingRange = distanceToPlayer < repearingRange;
        }
        else
        {
            Debug.LogWarning("No GameObject found with tag 'Armory'.");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            OnKeyPressF();
        }
    }

    void OnKeyPressF()
    {
        Debug.Log("F key pressed."); // Log to confirm the method is called
        if (!isReapearingRange)
        {
            Debug.Log("Player is not in range.");
            return;
        }

        Player playerFile = player.GetComponent<Player>();
        if (playerFile != null)
        {
            playerFile.setIsGatheredMaterial(true);
            Debug.Log("Player's material gathered status set to true.");
        }
        else
        {
            Debug.LogError("Player component not found on the player Transform.");
        }

        // Destroy this GameObject (the cube)
        Destroy(gameObject);
        Debug.Log("Kamni destroyed.");
    }
}
