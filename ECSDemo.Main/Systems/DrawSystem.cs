using System;
using System.Linq;
using ECSDemo.Main.Components;

namespace ECSDemo.Main.Systems
{
    public class DrawSystem : ISystem
    {
        public void Update(int deltaTime, EntityManager manager)
        {
            var drawables = manager.GetEntitiesWithComponent<DrawableComponent>().OrderBy(c => c.ZIndex);

            foreach (var item in drawables)
            {
                var location = manager.GetComponent<LocationComponent>(item.Entity);

                if (location != null)
                {
                    Console.SetCursorPosition(location.XLoc, location.YLoc);
                    Console.Write(item.Icon);
                }
            }
        }
    }
}