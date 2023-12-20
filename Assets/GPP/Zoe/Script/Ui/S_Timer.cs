using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_Timer : MonoBehaviour
{
    public static S_Timer instance;

    public TextMeshProUGUI timerText;
    public float remainingTime;
    public float timeSinceBeggining;
    [HideInInspector] public float timeSpendPhase;
    private bool onlyOnce = false;

    [Header("Texte End Game Lose")]
    public float timeEndMessageLose;
    public GameObject textEndGameLoseGO;
    public TextMeshProUGUI textEndGameLoseUI;
    public string textEndGameLose;

    public Image timerCircle;

    public Gradient grad;

    public Slider slider;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        textEndGameLoseGO.SetActive(false);
        remainingTime = GameMode.instance.settings[0].timePhase;
        //if (slider != null )
        slider.maxValue = remainingTime;
        timerCircle.color = grad.Evaluate(0);
        this.gameObject.SetActive(false);
    }
    void Update()
    {
        if(!onlyOnce)
        {
            timeSinceBeggining += Time.deltaTime;
            timeSpendPhase += Time.deltaTime;
            if (remainingTime <= 0.1f)
            {
                    GameMode.instance.stats.timePhases[GameMode.instance.currentPhase - 1] = (int)timeSpendPhase;
                    //Time.timeScale = 0;
                    textEndGameLoseGO.SetActive(true);
                    textEndGameLoseUI.text = textEndGameLose;
                    Invoke("EndGame", timeEndMessageLose);
                    onlyOnce = true;
            }
            else
            {
                remainingTime -= Time.deltaTime;
                int minutes = Mathf.FloorToInt(remainingTime / 60);
                int seconds = Mathf.FloorToInt(remainingTime % 60);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

                //if(slider != null )
                slider.value = remainingTime;
                timerCircle.color = grad.Evaluate(remainingTime/slider.maxValue);
            }
        }

    }

    public void EndGame()
    {
        GameMode.instance.EndGame();
    }


    public void TimerNextPhase(PhaseSettings settings)
    {
        GameMode.instance.stats.timePhases[GameMode.instance.currentPhase - 1] = (int)timeSpendPhase; // ajoute le temps aux stats
        timeSpendPhase = 0;
        remainingTime = settings.timePhase;
        slider.maxValue = remainingTime;
    }
}
