using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionWaypoint : MonoBehaviour
{
    public static MissionWaypoint instance;
    public Image img;
    public Transform target;
    public Vector3 offset;
    public List<GameObject> etabli;
    public GameObject gardenPoint;

    public float colorAlphaLow = 75f;
    public float colorAlphaHigh;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        LowOpacity();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMode.instance.currentPhase == 1)
        {
            target = etabli[0].transform;
        }
        else if (GameMode.instance.currentPhase == 2)
        {
            if (S_SoundManager.instance.isInGarden)
            {
                target = gardenPoint.transform;
            }
            else
            {
                target = etabli[1].transform;
            }
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

        //if (Vector3.Dot((target.position - transform.position), transform.forward) < 0)
        
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        if(Vector3.Dot(Camera.main.transform.forward, target.position - Camera.main.transform.position) < 2)
        {
            //if (pos.x < Screen.width / 2)
            //{
            //    pos.x = maxX;
            //}
            //else
            //{
            //    pos.x = minX;
            //}
            pos.x = Screen.width - pos.x;
            pos.y = minY;

        }

        img.transform.position = pos;
    }

    public void HighOpacity()
    {
        Color imgColorHighOp = img.color;
        imgColorHighOp = new Color(imgColorHighOp.r, imgColorHighOp.g, imgColorHighOp.b, colorAlphaHigh);
        img.color = imgColorHighOp;
    }

    public void LowOpacity()
    {
        Color imgColorLowOp = img.color;
        imgColorLowOp = new Color(imgColorLowOp.r, imgColorLowOp.g, imgColorLowOp.b, colorAlphaLow);
        img.color = imgColorLowOp;
    }
}
