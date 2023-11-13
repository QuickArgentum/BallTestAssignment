using UnityEngine;

[CreateAssetMenu(menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [Range(0, 1)]
    public float energyPerShot;
    public float projectileVelocity;
}