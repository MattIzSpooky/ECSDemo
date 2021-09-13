using System;
using System.Linq;
using ECSDemo.Main.Components;
using ECSDemo.Main.Utils;

namespace ECSDemo.Main.Systems
{
    public class AiSystem : ISystem
    {

        private readonly Random _random = new();
        
        public void Update(int deltaTime, EntityManager manager)
        {
            foreach (var item in manager.GetEntitiesWithComponent<AiComponent>())
            {
                var move = manager.GetComponent<MovableComponent>(item.Entity);
                var floors = manager.GetEntitiesWithComponent<FloorComponent>();
                var loc = manager.GetComponent<LocationComponent>(item.Entity);

                var direction = move.CurrentDirection;
                if (!direction.HasValue)
                {
                    direction = (Direction)_random.Next(Enum.GetValues(typeof(Direction)).Length);
                }

                int nextX, nextY;
                
                GetNextXY(move, loc, out nextX, out nextY, direction.Value);
                
                // When no floor on next space in current direction: 
                // 1. jGo for the first free floor that isn't the old floor
                // 2. In case of no candidate, turn around
                if (!floors.Any(f => f.XLoc == nextX && f.YLoc == nextY))
                {
                    var found = false;

                    foreach (Direction newDirection in Enum.GetValues(typeof(Direction)))
                    {
                        if (newDirection != direction && newDirection != direction.GetOpposite())
                        {
                            GetNextXY(move, loc, out nextX, out nextY, newDirection);

                            if (floors.Any(f => f.XLoc == nextX && f.YLoc == nextY))
                            {
                                found = true;
                                move.CurrentDirection = newDirection;
                                break;
                            }
                        }
                    }

                    if (!found)
                    {
                        move.CurrentDirection = direction.GetOpposite();
                    }
                }
            }
        }

        private static void GetNextXY(MovableComponent move, LocationComponent loc, out int nextX, out int nextY,
            Direction direction)
        {
             nextX = loc.XLoc + MovableComponent.NESW_XY[(int)direction, MovableComponent.XIndex] * move.XVelocity;
             nextY = loc.YLoc + MovableComponent.NESW_XY[(int)direction, MovableComponent.YIndex] * move.YVelocity;
        }
    }
}