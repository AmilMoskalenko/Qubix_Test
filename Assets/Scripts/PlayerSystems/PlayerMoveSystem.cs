using InputSystems;
using Leopotam.Ecs;
using UnityEngine;

namespace PlayerSystems
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private Config _config;
        private EcsFilter<Player, InputData> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var player = ref _filter.Get1(i);
                ref var input = ref _filter.Get2(i);
                if (input.Delay < _config.Delay && input.MoveInput != Direction.None)
                {
                    player.Transform.position = Vector3.Lerp(player.Transform.position,
                        player.Target, input.Delay * _config.MovingSpeed);
                    player.Transform.rotation = Quaternion.Lerp(player.Transform.rotation, 
                        Quaternion.LookRotation(player.RotateDirection), input.Delay * _config.RotationSpeed);
                }
            }
        }
    }
}
