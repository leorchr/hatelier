using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_ChangeScene : MonoBehaviour
{
    public string targetScene;
    // Start is called before the first frame update
    public void GoTo()
    {
        SceneManager.LoadScene(targetScene);
    }
}
