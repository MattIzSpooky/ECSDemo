namespace ECSDemo.Main.Utils
{
    public enum Direction
    {
        North = 0, East, South, West
    }

    public static class DirectionExtensions
    {
        public static Direction? GetOpposite(this Direction? direction)
        {
            if (!direction.HasValue) return null;
            
            var newVal = ((int)direction.Value + 2) % 4;
            Direction? toReturn = (Direction)newVal;

            return toReturn;
        }
    }
}