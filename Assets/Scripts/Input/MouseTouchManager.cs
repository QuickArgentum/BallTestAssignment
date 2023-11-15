using System;
using Zenject;

namespace Input
{
    /// <summary>
    /// Manages screen tap events on desktop platforms
    /// </summary>
    public class MouseTouchManager : ITouchManager, ITickable
    {
        public event Action OnTapStart;
        public event Action OnTapEnd;

        void ITickable.Tick()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
                OnTapStart?.Invoke();
            else if (UnityEngine.Input.GetMouseButtonUp(0))
                OnTapEnd?.Invoke();
        }
    }
}