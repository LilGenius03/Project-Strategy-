using System.Collections;
using TMPro;
using UnityEngine;

public enum GamePhases
{
    game_begin,
    round_begin,
    prep,
    combat,
    round_over,
    game_over
}

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

    public GamePhases current_phase;

    [SerializeField] GameObject camera_p1, camera_p2;

    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float countdownTime;

    [SerializeField] float prep_time = 15f;
    [SerializeField] float combat_time = 60f;
    [SerializeField] float win_time = 15f;
    [SerializeField] float round_win_time = 15f;

    public int current_round;
    public int max_rounds_needed;
    private Coroutine combat_coroutine;

    public bool p2_defending;

    public static int p1_score;
    public static int p2_score;

    //SHITE - WILL IT JUST BE TEMP? PROBS NOT 


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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SwitchSides()
    {
        p2_defending = !p2_defending;
        StartCoroutine(Start_Round());
    }

    public void StartGame()
    {
        StartCoroutine(stupiddelaythingyyo());
    }

    public IEnumerator stupiddelaythingyyo()
    {
        yield return new WaitForEndOfFrame();
        OnGameBegin?.Invoke();
        StartCoroutine(Start_Round());
    }

    IEnumerator Start_Round()
    {
        current_phase = GamePhases.round_begin;
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
        current_phase = GamePhases.prep;
        OnUnFreezePlayers?.Invoke();
        OnPrepBegin?.Invoke();

        countdownTime = prep_time;
        timerText.gameObject.SetActive(true);
        while (countdownTime > 3f)
        {
            timerText.text = countdownTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdownTime--;
        }
        countdownText.gameObject.SetActive(true);
        while (countdownTime > 0)
        {
            timerText.text = countdownTime.ToString();
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdownTime--;
        }
        countdownText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        countdownTime = 3f;

        OnPrepOver?.Invoke();
        combat_coroutine = StartCoroutine(Combat_Phase());
    }

    IEnumerator Combat_Phase()
    {
        current_phase = GamePhases.combat;
        OnCombatBegin?.Invoke();

        countdownTime = combat_time;
        timerText.gameObject.SetActive(true);
        while (countdownTime > 3f)
        {
            timerText.text = countdownTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdownTime--;
        }
        countdownText.gameObject.SetActive(true);
        while (countdownTime > 0)
        {
            timerText.text = countdownTime.ToString();
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdownTime--;
        }
        countdownText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        countdownTime = 3f;

        OnCombatOver?.Invoke();
        OnFreezePlayers?.Invoke();
        SwitchSides();
    }

    public void CastleDestroyed(int winner)
    {
        StartCoroutine(End_Round(winner));
    }

    IEnumerator End_Round(int round_winner)
    {
        current_phase = GamePhases.round_over;
        StopCoroutine(combat_coroutine);
        if (round_winner == 0)
            p1_score++;
        else if (round_winner == 1)
            p2_score++;

        countdownTime = 3f;
        timerText.gameObject.SetActive(false);
        OnFreezePlayers?.Invoke();

        OnRoundOver?.Invoke(round_winner);

        yield return new WaitForSecondsRealtime(round_win_time);
        if(p1_score == max_rounds_needed)
            End_Game(round_winner);
        else if(p2_score == max_rounds_needed)
            End_Game(round_winner);
        else
        {
            //New Round Stuff
        }
    }

    void End_Game(int game_winner)
    {
        current_phase = GamePhases.game_over;
        OnGameOver?.Invoke(game_winner);
    }

    IEnumerator Win_Phase()
    {
        //OnCombatBegin.Invoke();
        yield return new WaitForSecondsRealtime(win_time);
        //OnCombatOver.Invoke();
    }

}
