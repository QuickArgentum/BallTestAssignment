using DataHolder;
using UnityEngine;
using View;

namespace Handlers
{
    public class PlayerVisualsHandler
    {
        private readonly PlayerView _player;

        public PlayerVisualsHandler(PlayerView player, GameStateHolder gameStateHolder)
        {
            _player = player;

            gameStateHolder.OnPlayerEnergyUpdated += OnEnergyUpdated;
        }

        private void OnEnergyUpdated(float value)
        {
            _player.Scale = Mathf.Clamp01(value);
            if (value <= 0)
                _player.PlayDeathAnimation();
        }
    }
}