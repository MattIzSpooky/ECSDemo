using ECSDemo.Main.Components;
using ECSDemo.Main.Utils;

namespace ECSDemo.Main.Systems
{
    public class UserMoveSystem : ISystem
    {
        public Direction? Direction { get; private set; }
        
        public UserMoveSystem(IGoDirectionInput input)
        {
            input.GoDirectionRequested += (_, e) => this.Direction = e.Direction;
        }

        
        public void Update(int deltaTime, EntityManager manager)
        {
            if (this.Direction.HasValue)
            {
                foreach (var item in manager.GetEntitiesWithComponent<MovableByUserComponent>())
                {
                    item.CurrentDirection = this.Direction;
                }
            }
        }
    }
}