using Zenject;

public class BulletPoolInstaller : MonoInstaller
{
    [UnityEngine.SerializeField]
    private BulletTracerFXPool _bulletPool;

    public override void InstallBindings()
    {
        Container.Bind<BulletTracerFXPool>().FromInstance(_bulletPool).NonLazy();
    }
}
