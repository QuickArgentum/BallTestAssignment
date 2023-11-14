using DG.Tweening;
using UnityEngine;

namespace View
{
    public class ExplosionView : View
    {
        [SerializeField] private new Renderer renderer;
        [SerializeField] private float explosionDuration;
        [SerializeField] private float fadeDuration;

        private Transform _transform;
        private Material _material;
        private Color _startColor;

        private void Awake()
        {
            _transform = transform;
            _material = renderer.material;
            _startColor = _material.color;
        }

        public void Explode(float scale)
        {
            DOTween.Sequence()
                .Append(_transform.DOScale(Vector3.one * scale, explosionDuration).SetEase(Ease.OutQuad))
                .Append(_material.DOFade(0, fadeDuration))
                .AppendCallback(Push)
                .Play();
        }

        public override void OnPop()
        {
            _transform.localScale = Vector3.zero;
            _material.color = _startColor;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ObstacleView>(out var obstacle))
                obstacle.Infect();
        }
    }
}