using DG.Tweening;
using UnityEngine;

namespace View
{
    /// <summary>
    /// Component attached to the player ball controlling its size and victory/loss animations
    /// </summary>
    public class PlayerView : View
    {
        [SerializeField] private Transform projectilePoint;
        [SerializeField] private Transform mesh;
        [SerializeField] private ParticleSystem deathEffect;
        [SerializeField] private float minScale;
        [SerializeField] private float victoryAnticipationDuration;
        [SerializeField] private float victoryFlyDuration;

        private Vector3 _originalScale;
        private Vector3 _minScale;

        private void Awake()
        {
            _originalScale = mesh.localScale;
            _minScale = new Vector3(minScale, minScale, minScale);
        }

        public float Scale
        {
            set => mesh.localScale = Vector3.Lerp(_minScale, _originalScale, value);
            get => mesh.localScale.x;
        }

        public Transform ProjectilePoint => projectilePoint;

        public Tween CreateVictoryTween(Vector3 destination)
        {
            return DOTween.Sequence()
                .Append(Transform.DOMove(Transform.position - Transform.forward, victoryAnticipationDuration))
                .Join(Transform.DOScaleZ(0.8f, victoryAnticipationDuration))
                .Append(Transform.DOMove(destination, victoryFlyDuration).SetEase(Ease.InCubic))
                .Join(Transform.DOScaleZ(1.5f, victoryFlyDuration / 2).SetEase(Ease.InQuad))
                .Join(Transform.DOScaleZ(1.0f, victoryFlyDuration / 2).SetEase(Ease.OutQuad))
                .AppendCallback(() => mesh.gameObject.SetActive(false));
        }

        public void PlayDeathAnimation()
        {
            Transform.DOScale(Vector3.zero, 0.25f);
            deathEffect.Play();
        }
    }
}