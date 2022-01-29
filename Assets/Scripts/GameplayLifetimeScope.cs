using VContainer;
using VContainer.Unity;

public class GameplayLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(FindObjectOfType<GameManager>());
    }
}