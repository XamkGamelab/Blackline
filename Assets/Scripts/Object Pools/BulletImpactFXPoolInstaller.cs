using Zenject;

public class BulletImpactFXPoolInstaller : MonoInstaller
{
    [UnityEngine.SerializeField]
    private BulletImpactFXPool _bulletImpactFXPool;

    public override void InstallBindings()
    {
        Container.Bind<BulletImpactFXPool>().FromInstance(_bulletImpactFXPool).NonLazy();
    }
}
