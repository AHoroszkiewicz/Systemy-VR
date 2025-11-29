using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class GameController : MonoBehaviour
{
    [SerializeField] float score;
    [SerializeField] float timeLeft;
    public UnityEvent<float> gameScore = new UnityEvent<float>();
    public UnityEvent<float> gameTimeLeft = new UnityEvent<float>();
    public static GameController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameScore.Invoke(score);
        gameTimeLeft.Invoke(timeLeft);
        StartCoroutine(UpdateTime());
    }

    private void Update()
    {
        
    }

    IEnumerator UpdateTime()
    {
        var waitTime = new WaitForSeconds(1f);
        while (timeLeft > 0)
        {
            timeLeft--;
            gameTimeLeft.Invoke(timeLeft);
            yield return waitTime;
        }
    }

    public void AddScore(float points)
    {
        score += points;
        gameScore.Invoke(score);
    }

    public void IncreaseTime(float time)
    {
        timeLeft += time;
    }
}