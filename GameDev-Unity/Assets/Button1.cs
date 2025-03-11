using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button1 : MonoBehaviour
{
    public void Play()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
