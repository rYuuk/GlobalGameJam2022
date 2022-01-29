using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        Menu,
        Game,
        Pause,
        Finish
    }

    public Action Paused;
    public State state;


    public void StartGame()
    {
        state = State.Game;
    }

    public void Finish()
    {
        state = State.Finish;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void Pause()
    {
        state = State.Pause;
        Paused?.Invoke();
    }
}