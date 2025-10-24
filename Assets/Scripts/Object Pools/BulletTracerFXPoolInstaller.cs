using Zenject;

public class BulletTracerFXPoolInstaller : MonoInstaller
{
    [UnityEngine.SerializeField]
    private BulletTracerFXPool _bulletPool;

    public override void InstallBindings()
    {
        Container.Bind<BulletTracerFXPool>().FromInstance(_bulletPool).NonLazy();
    }
}
