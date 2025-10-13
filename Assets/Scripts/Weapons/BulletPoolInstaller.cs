using Zenject;

public class BulletPoolInstaller : MonoInstaller
{
    [UnityEngine.SerializeField]
    private BulletPool _bulletPool;

    public override void InstallBindings()
    {
        Container.Bind<BulletPool>().FromInstance(_bulletPool).NonLazy();
    }
}
