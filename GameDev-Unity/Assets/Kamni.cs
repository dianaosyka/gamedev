using UnityEngine;

public class Kamni : MonoBehaviour
{
    public float repearingRange = 3f;
    public Transform player;
    private bool isReapearingRange = false;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        isReapearingRange = distanceToPlayer < repearingRange;

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
        playerFile.setIsGatheredMaterial(true);

        // Destroy this GameObject (the cube)
        Destroy(gameObject);
        Debug.Log("Kamni destroyed.");
    }
}
