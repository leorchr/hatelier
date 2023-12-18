using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public ASyncScene baseRoom;

    public static SceneLoader instance;
    // Start is called before the first frame update
    void Start()
    {
        if (!instance) instance = this;

        for (int i = SceneManager.loadedSceneCount - 1; i > 0; i--)
        {

            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).name);
            
        }

        SceneManager.LoadScene(baseRoom.sceneName, LoadSceneMode.Additive);
    }

    public void loadScene(ASyncScene sc)
    {
        SceneManager.LoadScene(sc.sceneName, LoadSceneMode.Additive);
    }

    public void unloadScene(ASyncScene sc) 
    {
        SceneManager.UnloadSceneAsync(sc.sceneName);
    }
}
