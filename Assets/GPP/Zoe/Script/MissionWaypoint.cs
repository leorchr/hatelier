using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class MissionWaypoint : MonoBehaviour
{
    public static MissionWaypoint instance;
    public Image img;
    public Transform target;
    public Vector3 offset;
    public List<GameObject> etabli;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameMode.instance.currentPhase == 1)
        {
            target = etabli[0].transform;
        }
        else if(GameMode.instance.currentPhase == 2)
        {
            target = etabli[1].transform;
        }
        else if (GameMode.instance.currentPhase == 3)
        {
            target = etabli[2].transform;
        }
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);

        /*if(Vector3.Dot((target.position - transform.position), transform.forward) < 0)
        {
            if(pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
            
        }*/

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
    }

    public void HideWaypoint()
    {
        img.gameObject.SetActive(false);
    }

    public void ShowWaypoint()
    {
        img.gameObject.SetActive(true);
    }
}
