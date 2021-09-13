using System;
using System.Linq;
using ECSDemo.Main.Components;

namespace ECSDemo.Main.Systems
{
    public class HudSystem : ISystem
    {

        public HudSystem(int cursorTop)
        {
            CursorTop = cursorTop;
        }

        public int CursorTop { get; set; }

        public void Update(int deltaTime, EntityManager manager)
        {
            var eaten = manager.GetEntitiesWithComponent<EatComponent>().Sum(e => e.CoinsEaten);
            
            Console.SetCursorPosition(0, CursorTop);
            Console.WriteLine($"Points: {eaten}");
        }
    }
}