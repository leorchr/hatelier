using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class S_WarmUpTimer : MonoBehaviour
{
    public static S_WarmUpTimer instance;


    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI playBegin;
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject warmUpTimer;
    [SerializeField] private GameObject beginTextGO;
    [SerializeField] private GameObject objectives;

    [SerializeField] private float timeTextBeforeBegin;
    [SerializeField] private float remainingTime;

    [SerializeField] private string textBegin;

    

    [HideInInspector]
    public bool gameBegin;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameBegin = false;
        beginTextGO.SetActive(false);
        warmUpTimer.SetActive(true);
    }
    void Update()
    {
        remainingTime -= Time.deltaTime;
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = ((int)remainingTime).ToString();
        if((int)remainingTime == 0 && !gameBegin)
        {
            objectives.GetComponent<Animator>().SetBool("OnScreen",true);
            warmUpTimer.SetActive(false);
            beginTextGO.SetActive(true);
            playBegin.text = textBegin;
            Invoke("ShowTimer", timeTextBeforeBegin);
        }
    }

    void ShowTimer()
    {
        beginTextGO.SetActive(false);
        timer.SetActive(true);
        gameBegin = true;
    }

}
