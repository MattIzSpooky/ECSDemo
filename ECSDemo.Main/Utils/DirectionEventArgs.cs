using System;

namespace ECSDemo.Main.Utils
{
    public class DirectionEventArgs : EventArgs
    {
        public Direction Direction { get; }

        public DirectionEventArgs(Direction direction)
        {
            Direction = direction;
        }
    }
}