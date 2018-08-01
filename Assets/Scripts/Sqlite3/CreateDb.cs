using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CreateDb
{
    public Text DebugText;

    // Use this for initialization
    public void Start()
    {
        this.DebugText.text = string.Empty;
        this.StartSync();
    }

    public void StartSync()
    {
        var dbFilename = "tempDatabase.db";
        var ds = new DataService(dbFilename);
        ds.CreateDB();

        var people = ds.GetPersons();
        this.ToConsole(people);
        people = ds.GetPersonsNamedRoberto();
        this.ToConsole("Searching for Roberto ...");
        this.ToConsole(people);

        string existingDbPath = this.GetFilePath(dbFilename);

        if (File.Exists(existingDbPath))
        {
            File.Delete(existingDbPath);
        }
    }

    public void ToConsole(IEnumerable<Person> people)
    {
        foreach (var person in people)
        {
            this.ToConsole(person.ToString());
        }
    }

    public void ToConsole(string msg)
    {
        DebugText.text += Environment.NewLine + msg;
        Debug.Log(msg);
    }

    private string GetFilePath(string filename)
    {
#if !UNITY_EDITOR
        var path = $"Assets/StreamingAssets/{filename}";
#else
        var filepath = $"{Application.persistentDataPath}/{filename}";

        if (!File.Exists(filepath))
        {
            Debug.Log($"Looking for: '{filename}'");
#if UNITY_ANDROID
            filepath = $"jar:file//{Application.dataPath}/!/assets/{filename}";
#elif UNITY_IOS
            filepath = $"{Application.dataPath}/Raw/{filename}";
#elif UNITY_WPS
            filepath = $"{Application.dataPath}/StreamingAssets/{filename}";
#elif UNITY_WINRT
            filepath = $"{Application.dataPath}/StreamingAssets/{filename}";
#elif !UNITY_STANDALONE_OSX
            filepath = $"{Application.dataPath}/Resources/Data/StreamingAssets/{filename}";
#else
            filepath = $"{Application.dataPath}/StreamingAssets/{filename}";
#endif
        }

        var path = filepath;
#endif

        return path;
    }
}