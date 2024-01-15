using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPELoader : MonoBehaviour
{
    public static GPELoader Instance;
    public List<GameObject> GPEListR1 = new List<GameObject>();
    public List<GameObject> GPEListR2 = new List<GameObject>();
    public List<GameObject> GPEListR3 = new List<GameObject>();
    public List<GameObject> GPEListR4 = new List<GameObject>();
    public List<GameObject> GPEListR5 = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null) { Instance = this; } else { Destroy(gameObject); }
    }

    public void Load(int i)
    {
        List<MeshRenderer> renderers = new List<MeshRenderer>();

        renderers = GetRenderers(i);

        foreach (MeshRenderer mr in renderers)
        {
            mr.enabled = true;
        }
        for (int j = 1; j <= 5; j++)
        {
            if(i != j)
            {
                Unload(j);
            }
        }
        
    }
    public void Unload(int i) {
        List<MeshRenderer> renderers = new List<MeshRenderer>();

        renderers = GetRenderers(i);
        foreach (MeshRenderer mr in renderers)
        {
            mr.enabled = false;
        }
    }

    List<MeshRenderer> GetRenderers(int i) {
        List<MeshRenderer> rs = new List<MeshRenderer>();
        switch (i)
        {
            case 1:
                foreach(GameObject go in GPEListR1)
                {
                    rs.Add(go.GetComponent<MeshRenderer>());
                }
                break;
            case 2:
                foreach (GameObject go in GPEListR2)
                {
                    rs.Add(go.GetComponent<MeshRenderer>());
                }
                break;
            case 3:
                foreach (GameObject go in GPEListR3)
                {
                    rs.Add(go.GetComponent<MeshRenderer>());
                }
                break;
            case 4:
                foreach (GameObject go in GPEListR4)
                {
                    rs.Add(go.GetComponent<MeshRenderer>());
                }
                break;
            case 5:
                foreach (GameObject go in GPEListR5)
                {
                    rs.Add(go.GetComponent<MeshRenderer>());
                }
                break;
            default: break;
        }
        return rs;
    }
}
