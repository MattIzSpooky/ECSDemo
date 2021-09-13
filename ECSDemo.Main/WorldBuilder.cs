using ECSDemo.Main.Components;
using ECSDemo.Main.Utils;

namespace ECSDemo.Main
{
    public class WorldBuilder
    {
        public WorldBuilder(EntityManager manager)
        {
            Manager = manager;
        }

        public EntityManager Manager { get; set; }

        public void CreateLevel(string[] level)
        {
            for (var y = 0; y < level.Length; y++)
            {
                for (var x = 0; x < level[y].Length; x++)
                {
                    switch (level[y][x])
                    {
                        case '#':
                            CreateWall(x, y);
                            break;
                        case '-':
                            CreateCoin(x, y);
                            CreateFloor(x, y);
                            break;
                        case '@':
                            CreateGhost(x, y);
                            CreateCoin(x, y);
                            CreateFloor(x, y);
                            break;
                        case 'O':
                            CreatePacman(x, y);
                            CreateFloor(x, y);
                            break;
                    }
                }
            }
        }

        private void CreatePacman(int x, int y, char character = 'O')
        {
            var entity = Manager.CreateEntity();
            Manager.AddComponent(new LocationComponent(entity, x, y));
            Manager.AddComponent(new DrawableComponent(entity, character, 3));
            Manager.AddComponent(new MovableByUserComponent(entity, 1, 1));
            Manager.AddComponent(new EatComponent(entity));
        }

        private void CreateFloor(int x, int y)
        {
            var entity = Manager.CreateEntity();
            Manager.AddComponent(new FloorComponent(entity, x, y));
            Manager.AddComponent(new DrawableComponent(entity, ' ', 0));
        }

        private void CreateGhost(int x, int y, char character = '@')
        {
            var entity = Manager.CreateEntity();
            Manager.AddComponent(new LocationComponent(entity, x, y));
            Manager.AddComponent(new DrawableComponent(entity, character, 3));
            Manager.AddComponent(new AiComponent(entity));
            Manager.AddComponent(new MovableComponent(entity, 1, 1, Direction.East));
        }

        private void CreateCoin(int x, int y)
        {
            var entity = Manager.CreateEntity();
            Manager.AddComponent(new LocationComponent(entity, x, y));
            Manager.AddComponent(new DrawableComponent(entity, '-', 2));
            Manager.AddComponent(new EatableComponent(entity));
        }

        private void CreateWall(int x, int y)
        {
            var entity = Manager.CreateEntity();
            Manager.AddComponent(new BlockingComponent(entity, x, y));
            Manager.AddComponent(new DrawableComponent(entity, '#', 2));
        }
    }
}