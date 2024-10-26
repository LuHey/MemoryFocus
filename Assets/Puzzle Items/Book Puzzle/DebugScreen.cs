using UnityEngine;
using TMPro; 
using System.Text;
using System.Diagnostics;

public class DebugScreen : MonoBehaviour
{
    public TMP_Text logText; 
    private StringBuilder logBuilder = new StringBuilder();

    private void OnEnable()
    {
        UnityEngine.Application.logMessageReceived += HandleLog;
    }

    private void OnDisable()
    {
        UnityEngine.Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {

        if (logText == null)
        {
            UnityEngine.Debug.LogWarning("LogText is not assigned.");
            return;
        }

        logBuilder.AppendLine(logString);
        if (logBuilder.Length > 5000)
        {
            logBuilder.Remove(0, logBuilder.Length - 5000);
        }
        logText.text = logBuilder.ToString();
    }
}
