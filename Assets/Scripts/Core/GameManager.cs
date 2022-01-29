using System;
using UnityEngine;
using VContainer;

public class GameManager : MonoBehaviour
{
    [Inject] private CheckpointData checkpointData;
    public enum State
    {
        Running,
        Pause,
        Finish
    }

    public Action Running;
    public Action Paused;
    public Action Finished;

    
    public State state;


    public void StartGame()
    {
        state = State.Running;
        Running?.Invoke();
    }

    public void Finish()
    {
        state = State.Finish;
        Finished?.Invoke();
    }

    public void SetCheckpointID(int id)
    {
        checkpointData.lastCheckpointID = id;
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