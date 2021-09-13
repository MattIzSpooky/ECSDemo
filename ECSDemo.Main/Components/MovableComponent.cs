using ECSDemo.Main.Utils;

namespace ECSDemo.Main.Components
{
    public class MovableComponent : EntityComponent
    {
        public int XVelocity { get; }
        public int YVelocity { get; }
        public Direction? CurrentDirection { get; set;  }
        
        public static readonly int XIndex = 1;
        public static readonly int YIndex = 0;
        public static readonly int[,] NESW_XY = { { -1, 0 }, { 0, 1 }, { 1, 0 }, { 0, -1 } };

        public MovableComponent(int entity, int xVelocity, int yVelocity, Direction? currentDirection = null) : base(entity)
        {
            XVelocity = xVelocity;
            YVelocity = yVelocity;
            CurrentDirection = currentDirection;
        }
    }
}