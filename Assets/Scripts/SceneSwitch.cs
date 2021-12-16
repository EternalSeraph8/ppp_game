using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public GameObject obj_to_remove;

    private AsyncOperation loadingOperation;

    public void GotoMenuScene()
    {
        //Empty DontDestroyOnLoad so the scene can properly reload
        if (obj_to_remove != null)
        {
            SceneManager.MoveGameObjectToScene(obj_to_remove, SceneManager.GetActiveScene());
        }

        SceneManager.LoadScene("menu");
    }

    public void GotoIglooScene()
    {
        //Empty DontDestroyOnLoad so the scene can properly reload
        if (obj_to_remove != null)
        {
            SceneManager.MoveGameObjectToScene(obj_to_remove, SceneManager.GetActiveScene());
        }

        SceneManager.LoadScene("igloo_interior");
    }

    public void GotoSecretScene()
    {
        //Empty DontDestroyOnLoad so the scene can properly reload
        if (obj_to_remove != null)
        {
            SceneManager.MoveGameObjectToScene(obj_to_remove, SceneManager.GetActiveScene());
        }        

        SceneManager.LoadScene("secret");
    }

    public void QuitGame()
    {
        //Because Application.Quit doesn't work in the editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
