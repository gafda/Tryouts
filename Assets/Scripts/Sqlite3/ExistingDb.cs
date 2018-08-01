using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExistingDb
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
        var ds = new DataService("existing.db");
        //ds.CreateDB();
        var people = ds.GetPersons();
        this.ToConsole(people);

        people = ds.GetPersonsNamedRoberto();
        this.ToConsole("Searching for Roberto ...");
        this.ToConsole(people);

        ds.CreatePerson();
        this.ToConsole("New person has been created");
        var p = ds.GetJohnny();
        this.ToConsole(p.ToString());

        ds.RemovePerson(p);
        this.ToConsole("Added person has been removed");
        this.ToConsole(people);
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
}