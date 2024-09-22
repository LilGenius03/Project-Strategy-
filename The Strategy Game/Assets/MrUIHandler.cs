using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MrUIHandler : MonoBehaviour
{
    public UnityEvent OnGameBegin, OnRoundBegin, OnPrepBegin, OnPrepEnd, OnCombatBegin, OnCombatEnd, OnRoundOver, OnGameOver;


    [SerializeField] TextMeshProUGUI score_p1, score_p2;

    private void OnEnable()
    {
        GameManager.OnGameBegin += Handle_GameBegin;
        GameManager.OnRoundBegin += Handle_RoundBegin;
        GameManager.OnPrepBegin += Handle_PrepBegin;
        GameManager.OnPrepOver += Handle_PrepOver;
        GameManager.OnCombatBegin += Handle_CombatBegin;
        GameManager.OnCombatOver += Handle_CombatOver;
        GameManager.OnRoundOver += Handle_RoundOver;
        GameManager.OnGameOver += Handle_GameOver;
    }

    private void OnDisable()
    {
        
    }

    public void Handle_GameBegin()
    {
        OnGameBegin.Invoke();
    }

    public void Handle_RoundBegin()
    {
        OnRoundBegin.Invoke();
    }

    public void Handle_PrepBegin()
    {
        OnPrepBegin.Invoke();
    }

    public void Handle_PrepOver()
    {
        OnPrepEnd.Invoke();
    }

    public void Handle_CombatBegin()
    {
        OnCombatBegin.Invoke();
    }

    public void Handle_CombatOver()
    {
        OnCombatEnd.Invoke();
    }

    public void Handle_RoundOver(int winner)
    {
        OnRoundOver.Invoke();
        score_p1.text = GameManager.instance.p1_score.ToString();
        score_p2.text = GameManager.instance.p2_score.ToString();
    }

    public void Handle_GameOver(int winner)
    {
        OnGameOver.Invoke();
    }

}
