using UnityEngine;
public class Stairs1 : MonoBehaviour
{
    public float repearingRange = 3f;
    public Transform player;
    public Material newMaterial; // Add a public variable to assign the new material in the Inspector
    private bool isReapearingRange = false;

    void Start()
    {
        if (newMaterial == null)
        {
            Debug.LogError("New material is not assigned in the Inspector.");
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        isReapearingRange = distanceToPlayer < repearingRange;
    }

    void OnMouseDown()
    {
        Debug.Log("OnMouseDown called."); // Log to confirm the method is called
        if (!isReapearingRange)
        {
            Debug.Log("Player is not in range.");
            return;
        }

        Player playerFile = player.GetComponent<Player>();
        if(playerFile.getIsGatheredMaterial() == false) return;

        // Find the GameObject tagged as "Stairs1"
        GameObject stairs = GameObject.FindWithTag("StairsBlock1");
        if (stairs != null) 
        {
            // Change the material of the stairs
            Renderer renderer = stairs.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = newMaterial;
                Debug.Log("Stairs material changed.");
            }
            else
            {
                Debug.LogError("Renderer component not found on the GameObject tagged 'Stairs1'.");
            }

            // Destroy this GameObject (the cube)
            Destroy(gameObject);
            Debug.Log("StairsBlock1 destroyed.");
            playerFile.setIsGatheredMaterial(false);
        }
        else
        {
            Debug.LogWarning("No GameObject found with tag 'Stairs1'.");
        }
    }
}
