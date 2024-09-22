using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityTargetEvent OnEnteredTrigger, OnExitTrigger;
    public string target_tag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(target_tag))
        {
            OnEnteredTrigger.Invoke(other.gameObject);
            Debug.Log("UNIT ENTERED!!!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(target_tag))
        {
            OnExitTrigger.Invoke(other.gameObject);
            Debug.Log("UNIT EXIT!!!");
        }
    }
}

[System.Serializable]
public class UnityTargetEvent : UnityEvent<GameObject> { }
