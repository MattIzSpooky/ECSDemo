using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ECSDemo.Main.Systems;

namespace ECSDemo.Main
{
    class Program
    {
        private readonly List<ISystem> _systems = new();
        private EntityManager _entityManager;

        private string[] _level =  new[]
        {
            "#######",
            "#O----#",
            "#####-#",
            "#-----#",
            "#-#####",
            "#-----#",
            "#######",
        };
        
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            var main = new Program();
            
            main.Run();
        }

        private void Run()
        {
            _entityManager = new EntityManager();
            var builder = new WorldBuilder(_entityManager);
            
            builder.CreateLevel(_level);

            var inputSystem = new UserInputSystem(); 

            inputSystem.ToggleMovingRequested += (_, _) => ToggleSystem<MoveSystem>();
            
            _systems.Add(inputSystem);
            _systems.Add(new DrawSystem());
            _systems.Add(new UserMoveSystem(inputSystem));
            _systems.Add(new MoveSystem());
            _systems.Add(new AiSystem());
            _systems.Add(new EatSystem());
            _systems.Add(new HudSystem(_level.Length + 2));
        
            RunGameLoop();
        }

        public void ToggleSystem<T>() where T : ISystem, new()
        {
            if (_systems.Any(s => s is T))
            {
                _systems.RemoveAll(s => s is T);
            }
            else
            {
                _systems.Add(new T());
            }
        }

        private void RunGameLoop()
        {
            var loops = 0;
            while (true)
            {
                foreach (var system in _systems.ToList())
                {
                    system.Update(0, _entityManager);
                }
                
                Console.SetCursorPosition(0, _level.Length + 4);
                Console.WriteLine($"Number of frames: {++loops} \t\t\t\t");
                Console.WriteLine($"Drawing: {_systems.Any(s => s is DrawSystem)} \t\t\t\t");
                Console.WriteLine($"Moving: {_systems.Any(s => s is MoveSystem)} \t\t\t\t");
                
                Thread.Sleep(16); // ~60 fps
            }
        }
    }
    
}