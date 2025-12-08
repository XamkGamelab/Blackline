using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IPlayerPosition>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IPlayerHealth>().FromComponentInHierarchy().AsSingle();
    }
}
