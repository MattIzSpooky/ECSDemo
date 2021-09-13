using System.Linq;
using ECSDemo.Main.Components;

namespace ECSDemo.Main.Systems
{
    public class EatSystem : ISystem
    {
        public void Update(int deltaTime, EntityManager manager)
        {
            foreach (var item in manager.GetEntitiesWithComponent<EatComponent>())
            {
                var locations = manager.GetEntitiesWithComponent<LocationComponent>();
                var curLoc = locations.FirstOrDefault(l => l.Entity == item.Entity);

                var othersAtLocation = locations.Where(l => l.XLoc == curLoc.XLoc && l.YLoc == curLoc.YLoc)
                    .Select(l => l.Entity);

                foreach (var eatable in manager.GetComponents<EatableComponent>(othersAtLocation))
                {
                    item.CoinsEaten++;
                    manager.RemoveEntity(eatable.Entity);
                }
            }
        }
    }
}