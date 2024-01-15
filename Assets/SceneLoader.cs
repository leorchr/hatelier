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
        if (baseRoom != null)
        {
            loadScene(baseRoom);
        }
    }

    public void loadScene(ASyncScene sc)
    {
        SceneManager.LoadScene(sc.sceneName, LoadSceneMode.Additive);
        GPELoader.Instance.Load(sc.id);

    }

    public void unloadScene(ASyncScene sc) 
    {
        SceneManager.UnloadSceneAsync(sc.sceneName);
    }
}
