using System;
using Const;

namespace DataHolder
{
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