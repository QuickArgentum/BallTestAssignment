using System;
using Zenject;

namespace Input
{
    public class TouchManager : ITouchManager, ITickable
    {
        public event Action OnTapStart;

        public event Action OnTapEnd;
        
        private bool _isTouchPresent;
        
        void ITickable.Tick()
        {
            if (UnityEngine.Input.touchCount == 0)
            {
                if (!_isTouchPresent)
                    return;
                
                _isTouchPresent = false;
                OnTapEnd?.Invoke();
            }
            else
            {
                if (_isTouchPresent)
                    return;

                _isTouchPresent = true;
                OnTapStart?.Invoke();
            }
        }
    }
}