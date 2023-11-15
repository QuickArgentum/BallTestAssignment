using Data;
using DataHolder;
using UnityEngine.SceneManagement;
using Zenject;

/// <summary>
/// Global game facade to control its state (to start and end it currently)
/// </summary>
public class GameFacade : IInitializable
{
    private readonly GameStateHolder _gameStateHolder;

    public GameFacade(GameStateHolder gameStateHolder)
    {
        _gameStateHolder = gameStateHolder;
    }
    
    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }

    void IInitializable.Initialize()
    {
        _gameStateHolder.GameState = GameState.FadingIn;
    }
}