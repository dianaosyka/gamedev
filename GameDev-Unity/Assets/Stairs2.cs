using UnityEngine;

public class Stairs2 : MonoBehaviour
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
        

        // Find the GameObject tagged as "Weapon"
        GameObject weapon = GameObject.FindWithTag("Weapon");
        if (weapon != null)
        {
            // Change the material of the weapon
            Renderer renderer = weapon.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = newMaterial;
                Debug.Log("Weapon material changed.");
            }
            else
            {
                Debug.LogError("Renderer component not found on the GameObject tagged 'Weapon'.");
            }
        }
        else
        {
            Debug.LogWarning("No GameObject found with tag 'Weapon'.");
        }

        // Find the GameObject tagged as "Shield"
        GameObject shield = GameObject.FindWithTag("Shield");
        if (shield != null)
        {
            // Change the material of the shield
            Renderer renderer = shield.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = newMaterial;
                Debug.Log("Shield material changed.");
            }
            else
            {
                Debug.LogError("Renderer component not found on the GameObject tagged 'Shield'.");
            }
        }
        else
        {
            Debug.LogWarning("No GameObject found with tag 'Shield'.");
        }

        // Destroy this GameObject (the cube)
        Destroy(gameObject);
        Debug.Log("StairsBlock1 destroyed.");
    }
}
