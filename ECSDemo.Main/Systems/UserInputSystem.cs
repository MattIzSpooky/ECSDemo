using System;
using ECSDemo.Main.Utils;

namespace ECSDemo.Main.Systems
{
    public class UserInputSystem : ISystem, IGoDirectionInput
    {
        public event EventHandler ToggleMovingRequested;
        public event EventHandler<DirectionEventArgs> GoDirectionRequested;
        
        protected virtual void OnToggleMovingRequested(EventArgs e)
        {
            ToggleMovingRequested?.Invoke(this, e);
        }
        
        protected virtual void OnGoDirectionRequested(DirectionEventArgs e)
        {
            GoDirectionRequested?.Invoke(this, e);
        }
        
        public void Update(int deltaTime, EntityManager manager)
        {
            if (!Console.KeyAvailable) return;
            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.Spacebar: OnToggleMovingRequested(EventArgs.Empty);
                    break;
                case ConsoleKey.UpArrow: OnGoDirectionRequested(new DirectionEventArgs(Direction.North));
                    break;
                case ConsoleKey.RightArrow: OnGoDirectionRequested(new DirectionEventArgs(Direction.East));
                    break;
                case ConsoleKey.DownArrow: OnGoDirectionRequested(new DirectionEventArgs(Direction.South));
                    break;
                case ConsoleKey.LeftArrow: OnGoDirectionRequested(new DirectionEventArgs(Direction.West));
                    break;
            }
        }
    }

    public interface IGoDirectionInput
    {
        event EventHandler<DirectionEventArgs> GoDirectionRequested;
    }
}