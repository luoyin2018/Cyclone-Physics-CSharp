using System;

namespace Cyclone
{
    public class EventArgs<T> : EventArgs
    {
        public T Data { get; }
        public EventArgs(T data)
        {
            Data = data; ;
        }

        public static EventArgs<T> Create(T data)
        {
            return new EventArgs<T>(data);
        }
    }
}
