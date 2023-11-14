using UnityEngine;

[CreateAssetMenu(menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Shooting")]
    [Range(0, 1)]
    public float energyPerShot;
    public float projectileVelocity;
    public float explosionScalePerEnergy;
    
    [Header("Obstacles")]
    public int obstacleGenerationAttempts;
    public float obstacleFieldWidth;
    public float obstacleFieldHeight;
    public float obstacleNoiseScale;
    [Range(0, 1)]
    public float obstacleNoiseBias;
}