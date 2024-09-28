using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI counter_gold;
    [SerializeField] GameObject container_attack;
    [SerializeField] TextMeshProUGUI counter_attack_standard, counter_attack_heavy, counter_attack_runner;
    [SerializeField] GameObject container_defend;
    [SerializeField] TextMeshProUGUI counter_defend_standard, counter_defend_bomber, counter_defend_obstacle;

    public void ManagePlayerUI(int ui_gold, int[] ui_attackunits, int[] ui_defendunits)
    {
        //gold ui
        counter_gold.text = ui_gold.ToString();
        //attack ui
        counter_attack_standard.text = ui_attackunits[0].ToString();
        counter_attack_heavy.text = ui_attackunits[1].ToString();
        counter_attack_runner.text = ui_attackunits[2].ToString();
        //defend ui
        counter_defend_standard.text = ui_defendunits[0].ToString();
        counter_defend_bomber.text = ui_defendunits[1].ToString();
        counter_defend_obstacle.text = ui_defendunits[2].ToString();
    }

    public void SwitchUI(bool to_attack)
    {
        if (to_attack)
        {
            container_attack.SetActive(true);
            container_defend.SetActive(false);
        }
        else
        {
            container_defend.SetActive(true);
            container_attack.SetActive(false);
        }
    }
}
