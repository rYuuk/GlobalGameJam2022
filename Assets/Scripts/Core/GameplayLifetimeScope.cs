using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Rendering;
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
        builder.RegisterInstance(FindObjectOfType<Player>());
        builder.RegisterInstance(FindObjectsOfType<Checkpoint>());
        builder.RegisterInstance(FindObjectsOfType<MonoBehaviour>().OfType<IResetable>().ToArray());
        builder.RegisterInstance(Camera.main);
        builder.RegisterInstance(FindObjectOfType<Volume>());
    }
}