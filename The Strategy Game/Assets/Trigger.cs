using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityTargetEvent OnEnteredTrigger, OnExitTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("attack_unit"))
        {
            OnEnteredTrigger.Invoke(other.gameObject);
            Debug.Log("UNIT ENTERED!!!");
        }
        Debug.Log("UNIT EXIT!!!");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("attack_unit"))
        {
            OnExitTrigger.Invoke(other.gameObject);
            Debug.Log("UNIT EXIT!!!");
        }
    }
}

[System.Serializable]
public class UnityTargetEvent : UnityEvent<GameObject> { }
