using DataHolder;
using View;

namespace Handlers
{
    public class PlayerVisualsHandler
    {
        private readonly PlayerView _player;
        private readonly float _scaleMultiplier;

        public PlayerVisualsHandler(PlayerView player, GameStateHolder gameStateHolder)
        {
            _player = player;
            _scaleMultiplier = _player.Scale;

            gameStateHolder.OnPlayerEnergyUpdated += OnEnergyUpdated;
        }

        private void OnEnergyUpdated(float value)
        {
            _player.Scale = _scaleMultiplier * value;
        }
    }
}