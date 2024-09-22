using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void FreezePlayers();
    public static event FreezePlayers OnFreezePlayers;
    public delegate void UnFreezePlayers();
    public static event UnFreezePlayers OnUnFreezePlayers;

    public delegate void CombatBegin();
    public static event CombatBegin OnCombatBegin;
    public delegate void CombatOver();
    public static event CombatOver OnCombatOver;

    public delegate void PrepBegin();
    public static event PrepBegin OnPrepBegin;
    public delegate void PrepOver();
    public static event PrepOver OnPrepOver;

    public delegate void RoundBegin();
    public static event RoundBegin OnRoundBegin;
    public delegate void RoundOver(int winner);
    public static event RoundOver OnRoundOver;

    public delegate void GameBegin();
    public static event GameBegin OnGameBegin;
    public delegate void GameOver(int winner);
    public static event GameOver OnGameOver;

    [SerializeField] GameObject camera_p1, camera_p2;

    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] float countdownTime;

    [SerializeField] float prep_time = 15f;
    [SerializeField] float combat_time = 60f;
    [SerializeField] float win_time = 15f;
    [SerializeField] float round_win_time = 15f;

    public int current_round;
    public int max_rounds_needed;
    private Coroutine combat_coroutine;

    public bool p2_defending;

    public int p1_score;
    public int p2_score;

    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of Game Manager found");
            return;
        }
        instance = this;
    }
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Start_Round());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Start_Round()
    {
        current_round++;
        OnFreezePlayers?.Invoke();
        if (p2_defending)
        {
            camera_p1.SetActive(false);
            camera_p2.SetActive(true);
            PlayerManager.instance.SetDefending(true);
        }
        else
        {
            camera_p2.SetActive(false);
            camera_p1.SetActive(true);
            PlayerManager.instance.SetDefending(false);
        }


        countdownText.gameObject.SetActive(true);
        OnRoundBegin?.Invoke();
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdownTime--;
        }
        countdownText.gameObject.SetActive(false);
        countdownTime = 3f;
        StartCoroutine(Prep_Phase());
    }

    IEnumerator Prep_Phase()
    {
        OnUnFreezePlayers?.Invoke();
        OnPrepBegin?.Invoke();
        yield return new WaitForSecondsRealtime(prep_time);
        OnPrepOver?.Invoke();
        combat_coroutine = StartCoroutine(Combat_Phase());
    }

    IEnumerator Combat_Phase()
    {
        OnCombatBegin?.Invoke();
        yield return new WaitForSecondsRealtime(combat_time);
        OnCombatOver?.Invoke();
        OnFreezePlayers?.Invoke();
        if (p2_defending)
            StartCoroutine(End_Round(1));
        else
            StartCoroutine(End_Round(0));
    }

    IEnumerator End_Round(int round_winner)
    {
        StopCoroutine(combat_coroutine);
        OnRoundOver?.Invoke(round_winner);
        if (round_winner == 0)
            p1_score++;
        else if (round_winner == 1)
            p2_score++;

        yield return new WaitForSecondsRealtime(round_win_time);
        if (p1_score == max_rounds_needed)
        {
            End_Game(0);
        }
        else if (p2_score == max_rounds_needed)
        {
            End_Game(1);
        }
        else
            StartCoroutine(Start_Round());
    }

    void End_Game(int game_winner)
    {
        OnGameOver?.Invoke(game_winner);
    }

    IEnumerator Win_Phase()
    {
        //OnCombatBegin.Invoke();
        yield return new WaitForSecondsRealtime(win_time);
        //OnCombatOver.Invoke();
    }

}
