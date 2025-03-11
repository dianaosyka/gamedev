using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button2 : MonoBehaviour
{
    public void Play()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(1);
    }

}