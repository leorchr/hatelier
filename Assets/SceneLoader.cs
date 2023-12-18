using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public ASyncScene baseRoom;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = SceneManager.loadedSceneCount - 1; i > 0; i--)
        {

            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).name);
            
        }

        //SceneManager.LoadScene(baseRoom.sceneName, LoadSceneMode.Additive);
    }
}
