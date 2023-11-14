using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Shooting")]
        public float projectileVelocity;
        public float explosionScalePerEnergy;
        public float refireInterval;
        public float minShotEnergy;
        public float maxShotEnergy;
        public float energyTransferRate;
        public AnimationCurve energyTransferCurve;
    
        [Header("Obstacles")]
        public int obstacleGenerationAttempts;
        public float obstacleFieldWidth;
        public float obstacleFieldHeight;
        public float obstacleNoiseScale;
        [Range(0, 1)]
        public float obstacleNoiseBias;
        public float obstacleDeathDelay;
    }
}