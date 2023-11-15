using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Simple ui element which blackens the screen while the game is starting and when it is ending
    /// </summary>
    public class FadePanelUI : UIBase
    {
        [SerializeField] private Image image;
        [SerializeField] private float fadeDuration;

        public Tween CreateFadeInTween()
        {
            return image.DOFade(1.0f, fadeDuration);
        }

        public Tween CreateFadeOutTween()
        {
            image.color = Color.black;
            return image.DOFade(0.0f, fadeDuration);
        }
    }
}