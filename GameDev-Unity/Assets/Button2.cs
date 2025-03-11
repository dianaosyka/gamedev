using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button2 : MonoBehaviour
{
    public void Play()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(1);
    }

}