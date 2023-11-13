using Const;
using DataHolder;

namespace Handlers
{
    public class GameOverHandler
    {
        private readonly GameStateHolder _gameStateHolder;

        public GameOverHandler(GameStateHolder gameStateHolder)
        {
            _gameStateHolder = gameStateHolder;
            gameStateHolder.OnPlayerEnergyUpdated += OnEnergyUpdated;
        }

        private void OnEnergyUpdated(float value)
        {
            if (value <= 0)
                _gameStateHolder.GameState = GameState.GameOverScreenShowing;
        }
    }
}