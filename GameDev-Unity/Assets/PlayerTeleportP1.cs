using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerTeleportP1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Portal1"){
            gameObject.transform.position = new Vector3(-37f, 0f, -22.03f);
        }
        if(other.gameObject.tag == "Portal2"){
            gameObject.transform.position = new Vector3(22.03f, 0f, 37f);
            gameObject.transform.rotation = new Quaternion(0f, 90f, 0f, 0f);
        }
        if(other.gameObject.tag == "Ocean"){
            gameObject.transform.position = new Vector3(51.82f, -0.78f, -50.96f);
        }if(other.gameObject.tag == "PortalEnd"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
