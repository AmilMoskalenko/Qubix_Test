using System.Linq;
using Leopotam.Ecs;
using PlayerSystems;
using UnityEngine;

namespace InputSystems
{
    public class InputSystem : IEcsRunSystem
    {
        private Config _config;
        private EcsFilter<Player, InputData> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var player = ref _filter.Get1(i);
                ref var input = ref _filter.Get2(i);
                if (input.Delay < _config.Delay)
                {
                    input.Delay += Time.fixedDeltaTime;
                }
                if (input.Delay >= _config.Delay)
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        input.Delay = 0;
                        input.MoveInput = Direction.Up;
                        player.MoveDirection = input.MoveLock.All(l => l != Direction.Up) ?
                            Vector3.forward : Vector3.zero;
                        player.Reverse = false;
                        player.RotateDirection = Vector3.left;
                    }
                    if (Input.GetKey(KeyCode.A))
                    {
                        input.Delay = 0;
                        input.MoveInput = Direction.Left;
                        player.MoveDirection = input.MoveLock.All(l => l != Direction.Left) ?
                            Vector3.left : Vector3.zero;
                        player.Reverse = false;
                        player.RotateDirection = Vector3.back;
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        input.Delay = 0;
                        input.MoveInput = Direction.Down;
                        player.MoveDirection = input.MoveLock.All(l => l != Direction.Down) ?
                            Vector3.back : Vector3.zero;
                        player.Reverse = false;
                        player.RotateDirection = Vector3.right;
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        input.Delay = 0;
                        input.MoveInput = Direction.Right;
                        player.MoveDirection = input.MoveLock.All(l => l != Direction.Right) ?
                            Vector3.right : Vector3.zero;
                        player.Reverse = false;
                        player.RotateDirection = Vector3.forward;
                    }
                }
            }
        }
    }
}
