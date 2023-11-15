using DataHolder;
using UnityEngine;
using View;

namespace Handlers
{
    /// <summary>
    /// Responsible for changing the appearance of the player ball according to its current energy
    /// </summary>
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