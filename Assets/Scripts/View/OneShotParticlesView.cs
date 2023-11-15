using System.Collections;
using UnityEngine;

namespace View
{
    /// <summary>
    /// Component for creating simple poolable particle systems. GameObject will be returned to the pool
    /// when particle system finishes playing
    /// </summary>
    public class OneShotParticlesView : View
    {
        [SerializeField] private new ParticleSystem particleSystem;

        public override void OnPop()
        {
            particleSystem.Play();
            StartCoroutine(PushCoroutine());
        }

        private IEnumerator PushCoroutine()
        {
            yield return new WaitForSeconds(particleSystem.main.duration);
            Push();
        }
    }
}