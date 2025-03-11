using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    private bool isGatheredMaterial = false;
    public TextMeshPro textMaterials;

    void Start()
    {
        
    }

    void Update()
    {
        if (isGatheredMaterial)
        {
            textMaterials.text = "Materials: 1 hammer, 10 rocks";
        }
        else
        {
            textMaterials.text = "";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Portal1")
        {
            gameObject.transform.position = new Vector3(-37f, 0f, -22.03f);
            gameObject.transform.rotation = Quaternion.Euler(0f, -90f, 0f); // Rotate to the left
        }
        if (other.gameObject.tag == "Portal2")
        {
            gameObject.transform.position = new Vector3(22.03f, 0f, 37f);
            gameObject.transform.rotation = Quaternion.Euler(0f, 90f, 0f); // Rotate to the right
        }
        if (other.gameObject.tag == "Ocean")
        {
            gameObject.transform.position = new Vector3(51.82f, -0.78f, -50.96f);
        }
        if (other.gameObject.tag == "PortalEnd")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public bool getIsGatheredMaterial()
    {
        return isGatheredMaterial;
    }

    public void setIsGatheredMaterial(bool isGatheredMaterial)
    {
        this.isGatheredMaterial = isGatheredMaterial;
    }
}
