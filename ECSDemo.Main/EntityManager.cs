using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ECSDemo.Main.Components;

namespace ECSDemo.Main
{
    public class EntityManager
    {
        public List<int> Entities { get; set; } = new();
        public Dictionary<string, EntityComponent[]> Components { get; set; } = new();

        public int CreateEntity()
        {
            var entity = 0;

            if (Entities.Any())
                entity = Entities.Max() + 1;

            Entities.Add(entity);

            return entity;
        }

        public void RemoveEntity(int entity)
        {
            Entities.Remove(entity);

            foreach (var key in Components.Keys)
            {
                Components[key][entity] = null;
            }
        }

        public void AddComponent(EntityComponent component)
        {
            var type = component.GetType();
            
            // Support component inheritance
            while (type != null && !type.IsAbstract)
            {
                var name = type.Name;

                if (!Components.ContainsKey(name))
                {
                    Components[name] = new EntityComponent[10000];
                }
            
                Components[name][component.Entity] = component;

                type = type.BaseType;
            }
        }

        public IEnumerable<T> GetEntitiesWithComponent<T>() where T : EntityComponent
        {
            var name = typeof(T).Name;

            if (Components.ContainsKey(name))
            {
                return Components[name].Where(c => c != null).Cast<T>();
            }

            return Enumerable.Empty<T>();
        }

        public T GetComponent<T>(int entity)  where T : EntityComponent
        {
            var name = typeof(T).Name;

            if (Components.ContainsKey(name))
            {
                return (T)Components[name][entity];
            }

            return null;
        }

        public IEnumerable<T> GetComponents<T>(IEnumerable<int> othersAtLocation) where T : EntityComponent
        {
            var name = typeof(T).Name;

            if (Components.ContainsKey(name))
            {
                foreach (var entity in othersAtLocation)
                {
                    var component = (T)Components[name][entity];

                    if (component != null)
                    {
                        yield return component;
                    }
                }
            }
        }
    }
}