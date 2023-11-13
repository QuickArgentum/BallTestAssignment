using DataHolder;

namespace Handlers
{
    public class GameOverHandler
    {
        private readonly GameFacade _facade;

        public GameOverHandler(GameStateHolder gameStateHolder, GameFacade facade)
        {
            _facade = facade;

            gameStateHolder.OnPlayerEnergyUpdated += OnEnergyUpdated;
        }

        private void OnEnergyUpdated(float value)
        {
            if (value <= 0)
                _facade.RestartGame();
        }
    }
}