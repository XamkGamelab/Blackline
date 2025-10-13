using Zenject;

public class GameManagerInstaller : MonoInstaller
{
    [UnityEngine.SerializeField]
    private GameManager _gameManager;

    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromInstance(_gameManager).NonLazy();
    }
}
