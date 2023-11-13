using System;

namespace Input
{
    public interface ITouchManager
    {
        public event Action OnTapStart;

        public event Action OnTapEnd;
    }
}