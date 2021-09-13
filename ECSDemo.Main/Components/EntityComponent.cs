namespace ECSDemo.Main.Components
{
    public abstract class EntityComponent
    {
        public int Entity { get; }

        public EntityComponent(int entity)
        {
            Entity = entity;
        }
    }
}