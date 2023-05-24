using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevToolScript : MonoBehaviour
{
    [SerializeField] private bool displayLogs = true;
    void Start()
    {
        Debug.unityLogger.logEnabled = displayLogs;
    }

}
