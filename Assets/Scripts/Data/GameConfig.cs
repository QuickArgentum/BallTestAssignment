using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Player energy is always a value between 0 and 1 which starts at 0\n\nShooting")]
        [Tooltip("How quickly the player projectile moves")]
        public float projectileVelocity;
        [Tooltip("This value will be multiplied by projectile energy to get the size of the explosion")]
        public float explosionScalePerEnergy;
        [Tooltip("Minimal allowed time between creation of projectiles")]
        public float refireInterval;
        [Tooltip("Minimum allowed projectile energy")]
        public float minShotEnergy;
        [Tooltip("Maximum allowed projectile energy")]
        public float maxShotEnergy;
        [Tooltip("Amount of energy transferred from player to projectile per frame while holding the screen." +
                 "Affected by the following curve")]
        public float energyTransferRate;
        [Tooltip("Energy transfer multiplier depending on the time the screen is held in seconds")]
        public AnimationCurve energyTransferCurve;
    
        [Header("Obstacles")]
        [Tooltip("How many obstacles will the game attempt to generate. Mitigated by noise values")]
        public int obstacleGenerationAttempts;
        [Tooltip("Width of the area where obstacles are generated")]
        public float obstacleFieldWidth;
        [Tooltip("Height of the area where obstacles are generated")]
        public float obstacleFieldHeight;
        [Tooltip("Scale of the Perlin noise which affects probability of obstacle generation")]
        public float obstacleNoiseScale;
        [Range(0, 1)]
        [Tooltip("Minimum probability that the obstacle will be generated. 0 means pure noise," 
                 + "1 means obstacles are always generated disregarding the noise")]
        public float obstacleNoiseBias;
        [Tooltip("Delay in seconds between the obstacle is infected and it is destroyed")]
        public float obstacleDeathDelay;
    }
}