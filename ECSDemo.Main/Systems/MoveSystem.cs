using System.Linq;
using ECSDemo.Main.Components;

namespace ECSDemo.Main.Systems
{
    public class MoveSystem : ISystem
    {
        public void Update(int deltaTime, EntityManager manager)
        {
            var movables = manager.GetEntitiesWithComponent<MovableComponent>()
                .Where(mc => mc.CurrentDirection.HasValue);

            foreach (var item in movables)
            {
                var direction = (int)item.CurrentDirection.Value;

                var loc = manager.GetComponent<LocationComponent>(item.Entity);

                var nextX = loc.XLoc + MovableComponent.NESW_XY[direction, MovableComponent.XIndex] * item.XVelocity;
                var nextY = loc.YLoc + MovableComponent.NESW_XY[direction, MovableComponent.YIndex] * item.YVelocity;

                if (manager.GetEntitiesWithComponent<FloorComponent>().Any(f => f.XLoc == nextX && f.YLoc == nextY))
                {
                    loc.XLoc = nextX;
                    loc.YLoc = nextY;
                }
            }
        }
    }
}