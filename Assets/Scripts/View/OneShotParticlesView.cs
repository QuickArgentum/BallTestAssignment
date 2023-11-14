using System.Collections;
using UnityEngine;

namespace View
{
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