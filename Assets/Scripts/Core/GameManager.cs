using System;
using DefaultNamespace;
using UnityEngine;
using VContainer;

public class GameManager : MonoBehaviour
{
    [Inject] private CheckpointData checkpointData;
    [Inject] private Checkpoint[] checkpoints;
    [Inject] private PlayerLight playerLight;
    [Inject] private IResetable[] resetableComponents;

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

    public void Continue()
    {
        MovePlayerToLastCheckpoint();
    }

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

    private void OnEnable()
    {
        playerLight.LightConsumed += OnLightConsumed;
    }

    private void OnDisable()
    {
        playerLight.LightConsumed -= OnLightConsumed;
    }

    private void OnLightConsumed()
    {
        MovePlayerToLastCheckpoint();

        foreach (var component in resetableComponents)
        {
            component.ResetState();
        }
    }

    private void MovePlayerToLastCheckpoint()
    {
        var checkpoint = Array.Find(checkpoints, x => x.Id == checkpointData.lastCheckpointID);
        playerLight.transform.position = (checkpoint.SpawnPosition);
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
