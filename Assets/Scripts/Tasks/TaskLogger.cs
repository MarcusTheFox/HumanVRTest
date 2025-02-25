using System;
using System.IO;
using UnityEngine;

public static class TaskLogger
{
    private static string logFilePath;

    static TaskLogger()
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        logFilePath = Path.Combine(Application.persistentDataPath, $"TaskLog_{timestamp}.txt");
        File.WriteAllText(logFilePath, "Task Log - Started at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n");
        Debug.Log("TaskLogger initialized: " + logFilePath);
    }

    public enum EventType
    {
        Entered,
        PartiallyCompleted,
        Completed,
        Exited
    }

    public static void LogEvent(string taskName, EventType eventType, string customMessage, DateTime startTime)
    {
        DateTime timeNow = DateTime.Now;
        string timeString = timeNow.ToString("HH:mm:ss");
        string eventShort = eventType switch
        {
            EventType.Entered => "Entered",
            EventType.PartiallyCompleted => "Partial",
            EventType.Completed => "Completed",
            EventType.Exited => "Exited",
            _ => "Unknown"
        };
        TimeSpan timeElapsed = timeNow - startTime;
        string timeFormatted = $"({(int)timeElapsed.TotalMinutes:D}m {timeElapsed.Seconds:D2}s)";
        string logEntry = $"[{timeString}] [{taskName}({eventShort})] {timeFormatted}: {customMessage}\n";
        WriteToFile(logEntry);
    }

    public static void LogEvent(string customName, string customMessage, TimeSpan timeElapsed)
    {
        string timeString = DateTime.Now.ToString("HH:mm:ss");
        string timeFormatted = $"({(int)timeElapsed.TotalMinutes:D}m {timeElapsed.Seconds:D2}s)";
        string logEntry = $"[{timeString}] [{customName}] {timeFormatted}: {customMessage}\n";
        WriteToFile(logEntry);
    }

    public static void LogSeparator(string separator = "---")
    {
        WriteToFile(separator + "\n");
    }

    private static void WriteToFile(string logEntry)
    {
        try
        {
            File.AppendAllText(logFilePath, logEntry);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write to log file: {e.Message}");
        }
    }
}