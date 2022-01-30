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

    public Action Paused;
    public Action Finished;
    public Action Resumed;

    public State state;

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
        Invoke(nameof(ResetPlayer),3f);

        foreach (var component in resetableComponents)
        {
            component.ResetState();
        }
    }

    private void ResetPlayer()
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
        playerLight.transform.position = new Vector3(checkpoint.SpawnPosition.x,checkpoint.SpawnPosition.y,0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            if (state == State.Running)
            {
                Pause();    
            }
            else
            {
                Resume();
            }
        } 
    }

    private void Resume()
    {
        state = State.Running;
        Time.timeScale = 1f;
        Resumed?.Invoke();
    }
    
    private void Pause()
    {
        state = State.Pause;
        Time.timeScale = 0f;
        Paused?.Invoke();
    }
}
