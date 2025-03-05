using System;
using System.IO;
using UnityEngine;
using static Valve.VR.SteamVR_ExternalCamera;

public static class TaskLogger
{
    private static string logFilePath;

    [Serializable]
    private class TaskLoggerConfig
    {
        [NonSerialized]
        public string logConfigFolderPath;
        [NonSerialized]
        public string logConfigFilePath;
        public string editorLogFolderPath;
        public string buildLogFolderPath;

        public TaskLoggerConfig()
        {
            logConfigFolderPath = Path.Combine(Application.persistentDataPath, "TaskLoggerConfig");
            logConfigFilePath = Path.Combine(logConfigFolderPath, "config.json");
        }

        public void CreateConfigFile()
        {
            editorLogFolderPath = Path.Combine(Application.persistentDataPath, "EditorTaskLogs");
            buildLogFolderPath = Path.Combine(Application.persistentDataPath, "BuildTaskLogs");
            string json = JsonUtility.ToJson(this);
            if (!Directory.Exists(logConfigFolderPath)) Directory.CreateDirectory(logConfigFolderPath);
            File.WriteAllText(logConfigFilePath, json);
        }
    }

    static TaskLogger()
    {
        TaskLoggerConfig config = new TaskLoggerConfig();

        string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        Debug.Log("Log config file path: " + config.logConfigFilePath);

        if (!File.Exists(config.logConfigFilePath))
        {
            config.CreateConfigFile();
        }
        else
        {
            string json = File.ReadAllText(config.logConfigFilePath);
            config = JsonUtility.FromJson<TaskLoggerConfig>(json);
            Debug.Log("Loaded config: " + config.editorLogFolderPath);
        }

        string logFolder;

#if UNITY_EDITOR
        logFolder = config.editorLogFolderPath;
#else
        logFolder = config.buildLogFolderPath;
#endif

        logFilePath = Path.Combine(logFolder, $"TaskLog_{timestamp}.txt");

        if (!Directory.Exists(logFolder))
        {
            Directory.CreateDirectory(logFolder);
        }
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