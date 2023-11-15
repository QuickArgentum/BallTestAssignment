using System;
using Data;

namespace DataHolder
{
    /// <summary>
    /// This object can be viewed as the model of the game. It is passive and does not have any internal logic
    /// and it only notifies of the changes made to it externally.
    /// Most game logic is controlled by listening to the changes made to the model and indirectly trigger other
    /// logic by making their own changes to it.
    /// </summary>
    public class GameStateHolder
    {
        public event Action<float> OnPlayerEnergyUpdated;
        
        private float _playerEnergy;
        public float PlayerEnergy
        {
            get => _playerEnergy;
            set
            {
                _playerEnergy = value;
                OnPlayerEnergyUpdated?.Invoke(value);
            }
        }

        public event Action<GameState> OnGameStateUpdated;

        private GameState _gameState;
        public GameState GameState
        {
            get => _gameState;
            set
            {
                _gameState = value;
                OnGameStateUpdated?.Invoke(value);
            }
        }

        public GameStateHolder()
        {
            _playerEnergy = 1.0f;
        }
    }
}