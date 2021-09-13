namespace ECSDemo.Main.Systems
{
    public interface ISystem
    {
        void Update(int deltaTime, EntityManager manager);
    }
}