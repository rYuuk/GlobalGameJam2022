using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayLifetimeScope : LifetimeScope
{
    [SerializeField] private GameData gameData;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(FindObjectOfType<GameManager>());
        builder.RegisterInstance<GameData>(gameData);
    }
}