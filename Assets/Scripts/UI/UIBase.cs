using UnityEngine;

namespace UI
{
    public class UIBase : MonoBehaviour
    {
        private GameObject _canvas;
        private GameObject Canvas => _canvas ? _canvas : _canvas = transform.parent.gameObject;

        public void SetEnabled(bool value)
        {
            Canvas.SetActive(value);
        }
    }
}