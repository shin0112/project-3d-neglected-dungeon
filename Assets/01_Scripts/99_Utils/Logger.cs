using System.Runtime.CompilerServices;
using UnityEngine;

public static class Logger
{
#if UNITY_EDITOR
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    [System.Diagnostics.DebuggerStepThrough]
    public static void Log(
        string message,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
        )
    {
        string className = System.IO.Path.GetFileNameWithoutExtension(filePath);
        Debug.Log($"[{className}.{memberName}:{lineNumber}] {message}");
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    [System.Diagnostics.DebuggerStepThrough]
    public static void LogWarning(
        string message,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
        )
    {
        string className = System.IO.Path.GetFileNameWithoutExtension(filePath);
        Debug.LogWarning($"[{className}.{memberName}:{lineNumber}] {message}");
    }
#endif
}
