using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayLifetimeScope : LifetimeScope
{
    [SerializeField] private CheckpointData checkpointData;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(FindObjectOfType<GameManager>());
        builder.RegisterInstance(checkpointData);
        builder.RegisterInstance(FindObjectOfType<PlayerLight>());
    }
}