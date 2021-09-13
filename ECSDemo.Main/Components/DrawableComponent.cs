namespace ECSDemo.Main.Components
{
    public class DrawableComponent : EntityComponent
    {
        public char Icon { get; }
        public int ZIndex { get; }

        public DrawableComponent(int entity, char icon, int zIndex) : base(entity)
        {
            Icon = icon;
            ZIndex = zIndex;
        }
    }
}