namespace ECSDemo.Main.Components
{
    public class EatComponent : EntityComponent
    {
        public EatComponent(int entity) : base(entity)
        {
        }
        
        public int CoinsEaten { get; set; }
    }
}