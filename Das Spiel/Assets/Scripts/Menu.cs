using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play(){
        SceneManager.LoadScene(0);
    }

    public void Exit(){
        Debug.Log("Exit");
        Application.Quit();
    }
}
