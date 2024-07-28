using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public delegate void TriggerCallbackDelegate();
    public TriggerCallbackDelegate TriggerEnterCallback;
    public TriggerCallbackDelegate TriggerExitCallback;

    void OnTriggerEnter2D(Collider2D collider)
    {
        TriggerEnterCallback?.Invoke();
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        TriggerExitCallback?.Invoke();
    }

}
