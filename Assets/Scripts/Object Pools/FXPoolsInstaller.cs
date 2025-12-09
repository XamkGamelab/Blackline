using Zenject;

public class FXPoolsInstaller : MonoInstaller
{
    [UnityEngine.SerializeField]
    private BulletImpactFXPool _bulletImpactFXPool;
    [UnityEngine.SerializeField]
    private PuddleFXPool _androidBloodPuddleFXPool;
    [UnityEngine.SerializeField]
    private PuddleFXPool _fleshBloodPuddleFXPool;

    public override void InstallBindings()
    {
        Container.Bind<BulletImpactFXPool>().FromInstance(_bulletImpactFXPool).NonLazy();
        Container.Bind<PuddleFXPool>().WithId(PuddleType.Android).FromInstance(_androidBloodPuddleFXPool).NonLazy();
        Container.Bind<PuddleFXPool>().WithId(PuddleType.Flesh).FromInstance(_fleshBloodPuddleFXPool).NonLazy();
    }
}
