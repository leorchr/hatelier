using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_FinalMenuUI : MonoBehaviour
{
    public void SceneToLoad(string sceneToLoad)
    {
        Destroy(GameObject.FindWithTag("Manager"));
        SceneManager.LoadScene(sceneToLoad);
    }
}