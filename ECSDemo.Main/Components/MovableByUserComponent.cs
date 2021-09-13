using ECSDemo.Main.Utils;

namespace ECSDemo.Main.Components
{
    public class MovableByUserComponent : MovableComponent
    {
        public MovableByUserComponent(int entity, int xVelocity, int yVelocity, Direction? currentDirection = null) : base(entity, xVelocity, yVelocity, currentDirection)
        {
        }
    }
}