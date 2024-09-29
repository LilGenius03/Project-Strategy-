using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public delegate void TurnBegin();
    public static event TurnBegin OnTurnBegin;
    public delegate void TurnOver();
    public static event TurnOver OnTurnOver;

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

    public int current_level;

    public int current_turn;
    public int current_round;
    public int max_rounds_needed;
    private Coroutine combat_coroutine;

    public bool p2_defending;

    public static int p1_score;
    public static int p2_score;

    public PlayerController p_attacker;

    public int dead_ppl;

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

    public void RanLevel()
    {
        int ran_lvl = Random.Range(1, 4);
        while (ran_lvl == current_level)
        {
            ran_lvl = Random.Range(1, 4);
        }
        LoadLevel(ran_lvl);
    }

    public void LoadLevel(int lvl)
    {
        current_level = lvl;
        SceneManager.LoadScene(lvl);
    }

    void SwitchSides()
    {
        p2_defending = !p2_defending;
        //StartCoroutine(Start_Round());
    }

    public void StartGame()
    {
        StartCoroutine(stupiddelaythingyyo());
    }

    public IEnumerator stupiddelaythingyyo()
    {
        yield return new WaitForEndOfFrame();
        OnGameBegin?.Invoke();
        Start_Round();
    }

    void Start_Round()
    {
        current_phase = GamePhases.round_begin;
        dead_ppl = 0;

        PlayerManager.instance.PayPlayers(current_round);

        current_round++;

        OnFreezePlayers?.Invoke();
        OnRoundBegin?.Invoke();
        StartCoroutine(Start_Turn());
    }

    IEnumerator Start_Turn()
    {
        current_turn++;

        dead_ppl = 0;

        OnFreezePlayers?.Invoke();

        if (p2_defending)
        {
            camera_p1.SetActive(false);
            camera_p2.SetActive(true);
            PlayerManager.instance.SetDefending(true);
            p_attacker = PlayerManager.instance.players[0].GetComponent<PlayerController>();
        }
        else
        {
            camera_p2.SetActive(false);
            camera_p1.SetActive(true);
            PlayerManager.instance.SetDefending(false);
            p_attacker = PlayerManager.instance.players[1].GetComponent<PlayerController>();
        }

        countdownText.gameObject.SetActive(true);
        OnTurnBegin?.Invoke();
        countdownTime = 3f;
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
        while (countdownTime > 0 && countdownTime <= 3f)
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
        countdownTime = 3f;
        countdownText.gameObject.SetActive(true);
        while (countdownTime > 0 && countdownTime <= 3f)
        {
            timerText.text = countdownTime.ToString();
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdownTime--;
        }
        countdownText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        countdownTime = 3f;
        StartCoroutine(End_Turn());
    }

    public void SomeoneDied()
    {
        dead_ppl++;
        if(dead_ppl == p_attacker.total_men)
        {
            StartCoroutine(End_Turn());
        }
    }

    IEnumerator End_Turn()
    {
        StopCoroutine(combat_coroutine);
        OnCombatOver?.Invoke();
        OnFreezePlayers?.Invoke();
        OnTurnOver?.Invoke();
        yield return new WaitForSecondsRealtime(round_win_time);
        SwitchSides();

        if (current_turn % 2 == 0)
            Start_Round();
        else
            StartCoroutine(Start_Turn());
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
            RanLevel();
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
