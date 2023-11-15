using DG.Tweening;
using UnityEngine;

namespace View
{
    /// <summary>
    /// Component attached to player explosions controlling their animation and obstacle infection logic
    /// </summary>
    public class ExplosionView : View
    {
        [SerializeField] private new Renderer renderer;
        [SerializeField] private float explosionDuration;
        [SerializeField] private float fadeDuration;

        private Material _material;
        private Color _startColor;

        private void Awake()
        {
            _material = renderer.material;
            _startColor = _material.color;
        }

        public void Explode(float scale)
        {
            DOTween.Sequence()
                .Append(Transform.DOScale(Vector3.one * scale, explosionDuration).SetEase(Ease.OutQuad))
                .Append(_material.DOFade(0, fadeDuration))
                .AppendCallback(Push)
                .Play();
        }

        public override void OnPop()
        {
            Transform.localScale = Vector3.zero;
            _material.color = _startColor;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ObstacleView>(out var obstacle))
                obstacle.Infect();
        }
    }
}