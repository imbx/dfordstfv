using UnityEngine;
using System;

public static class DBot
{
    public static void SendError(string scriptName, string error)
    {
        Debug.LogError("[DBOT - " + scriptName + "] " + error);
    }

    public static void SendLog(string scriptName, string message)
    {
        Debug.Log("[DBOT - " + scriptName + "] " + message);
    }

    public static void SendWarning(string scriptName, string warn)
    {
        Debug.LogWarning("[DBOT - " + scriptName + "] " + warn);
    }
}