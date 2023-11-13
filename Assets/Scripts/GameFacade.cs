using Const;
using DataHolder;
using UnityEngine.SceneManagement;
using Zenject;

public class GameFacade : IInitializable
{
    private readonly GameStateHolder _gameStateHolder;

    public GameFacade(GameStateHolder gameStateHolder)
    {
        _gameStateHolder = gameStateHolder;
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    void IInitializable.Initialize()
    {
        _gameStateHolder.GameState = GameState.FadingIn;
    }
}