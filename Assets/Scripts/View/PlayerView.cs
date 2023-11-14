using DG.Tweening;
using UnityEngine;

namespace View
{
    public class PlayerView : View
    {
        [SerializeField] private Transform projectilePoint;
        [SerializeField] private Transform mesh;
        [SerializeField] private float victoryAnticipationDuration;
        [SerializeField] private float victoryFlyDuration;

        public float Scale
        {
            set => mesh.localScale = new Vector3(value, value, value);
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
                .Join(Transform.DOScaleZ(1.0f, victoryFlyDuration / 2).SetEase(Ease.OutQuad));
        }
    }
}