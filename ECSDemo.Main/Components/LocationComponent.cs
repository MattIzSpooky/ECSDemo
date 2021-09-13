namespace ECSDemo.Main.Components
{
    public class LocationComponent : EntityComponent
    {
        public int XLoc { get; set; }
        public int YLoc { get; set; }

        public LocationComponent(int entity, int xLoc, int yLoc) : base(entity)
        {
            XLoc = xLoc;
            YLoc = yLoc;
        }
    }
}