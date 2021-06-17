using System;
using UnityEngine;

/// <summary>
/// Singleton Class to manage all events
/// Create an event Action and method to invoke the event.
/// In Observers classes Subscribe and Unsubscribe the neccesary methods 
///     Call the event asociated invoke methods from the place needed.
/// </summary>
public class Events : Singleton<Events>
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    /// <summary>
    /// Example event and invoke method of a opening door.
    /// </summary>
    public event Action onHighScoreChanged;
    public void NewHighScore()
    {
        // Check if there are methods subscribed to the event
        if (onHighScoreChanged != null)
        {
            // Invoke the event
            onHighScoreChanged();
        }
    }
}
