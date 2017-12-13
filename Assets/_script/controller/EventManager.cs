using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void PauseState(object o, PauseStateArgs e);
    public static event PauseState IsPaused;

    private static EventManager eventManager;
    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    //eventManager.Init();
                }
            }

            return eventManager;
        }
    }
}

public class PauseStateArgs : EventArgs
{
    private bool pause;

    public bool Pause
    {
        get
        {
            return pause;
        }
    }

    public PauseStateArgs(bool p)
    {
        pause = p;
    }
}
