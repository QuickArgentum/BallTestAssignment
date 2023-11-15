using UnityEngine;

namespace View
{
    /// <summary>
    /// Component attached to the destination
    /// </summary>
    public class DestinationView : View
    {
        [SerializeField] private Animation victoryAnimation;
        [SerializeField] private ParticleSystem victoryEffect;

        public void PlayVictoryAnimation()
        {
            victoryAnimation.Play();
            victoryEffect.Play();
        }
    }
}