using System;
using System.Linq;
using InputSystems;
using Leopotam.Ecs;
using PlayerSystems;

namespace EnvironmentSystems
{
    public class BorderSystem : IEcsRunSystem
    {
        private SceneData _sceneData;
        private EcsFilter<Player, InputData> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var player = ref _filter.Get1(i);
                ref var input = ref _filter.Get2(i);
                foreach (var border in _sceneData.Borders)
                {
                    if (Math.Abs(border.Transform.position.z - player.Transform.position.z) < 0.01f && border.Direction == Direction.Down)
                    {
                        if (input.MoveLock.All(l => l != Direction.Down))
                            input.MoveLock.Add(Direction.Down);
                    }
                    else if (Math.Abs(border.Transform.position.z - player.Transform.position.z) < 0.01f && border.Direction == Direction.Up)
                    {
                        if (input.MoveLock.All(l => l != Direction.Up))
                            input.MoveLock.Add(Direction.Up);
                    }
                    else if (Math.Abs(border.Transform.position.x - player.Transform.position.x) < 0.01f && border.Direction == Direction.Right)
                    {
                        if (input.MoveLock.All(l => l != Direction.Right))
                            input.MoveLock.Add(Direction.Right);
                    }
                    else if (Math.Abs(border.Transform.position.x - player.Transform.position.x) < 0.01f && border.Direction == Direction.Left)
                    {
                        if (input.MoveLock.All(l => l != Direction.Left))
                            input.MoveLock.Add(Direction.Left);
                    }
                    else
                    {
                        if (input.MoveLock.Count >= 1 && !player.Reverse)
                        {
                            if (input.MoveInput == Direction.Up)
                                input.MoveLock.Remove(Direction.Down);
                            if (input.MoveInput == Direction.Down)
                                input.MoveLock.Remove(Direction.Up);
                            if (input.MoveInput == Direction.Right)
                                input.MoveLock.Remove(Direction.Left);
                            if (input.MoveInput == Direction.Left)
                                input.MoveLock.Remove(Direction.Right);
                        }
                    }
                }
            }
        }
    }
}
