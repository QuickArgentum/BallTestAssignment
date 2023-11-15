using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// UI element which displays victory or loss message and prompts the user to tap the screen to restart the game
    /// </summary>
    public class GameOverPanelUI : UIBase
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text subtitle;
        [SerializeField] private float elementFadeDuration;

        private Color _color;

        private void Awake()
        {
            _color = image.color;                    
            image.color = new Color(_color.r, _color.g, _color.b, 0);
            title.alpha = 0;
            subtitle.alpha = 0;
        }

        public void SetTitle(string value)
        {
            title.text = value;
        }

        public Tween CreateFadeInTween()
        {
            return DOTween.Sequence()
                .Append(image.DOFade(_color.a, elementFadeDuration))
                .Append(title.DOFade(1, elementFadeDuration))
                .Append(subtitle.DOFade(1, elementFadeDuration));
        }

        public Tween CreateFadeOutTween()
        {
            return DOTween.Sequence()
                .Join(image.DOFade(0, elementFadeDuration))
                .Join(title.DOFade(0, elementFadeDuration))
                .Join(subtitle.DOFade(0, elementFadeDuration));
        }
    }
}