using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ShowSculpturePart : MonoBehaviour
{
    Vector3 startPos;
    float startScale;

    public Transform itemCenter;

    [SerializeField] private float timeToComplete;
    [SerializeField] private AnimationCurve posCurve;
    [SerializeField] private AnimationCurve sizeCurve;

    Image UIImage;

    private float t = 1;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startScale = transform.localScale.x;
        UIImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (t != 1)
        {
            t = Mathf.Clamp(t + Time.deltaTime / timeToComplete,0,1);
            transform.position = Vector3.Lerp(itemCenter.position, startPos, posCurve.Evaluate(t));
            float scale = Mathf.Lerp(10, startScale, sizeCurve.Evaluate(t));
            transform.localScale = new Vector3(scale, scale, scale);    
        }
    }

    public void setImage(Sprite sprite)
    {
        UIImage.sprite = sprite;
        t = 0;
    }
}
