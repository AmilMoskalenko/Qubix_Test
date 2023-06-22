using InputSystems;
using Leopotam.Ecs;
using UnityEngine;

namespace PlayerSystems
{
    public class PlayerTargetSystem : IEcsRunSystem
    {
        private Config _config;
        private EcsFilter<Player, InputData> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var player = ref _filter.Get1(i);
                ref var input = ref _filter.Get2(i);
                if (!player.MeleeWeapon.gameObject.activeSelf && player.MoveDirection != Vector3.zero)
                {
                    if (input.Delay == 0)
                    {
                        player.Target = player.Transform.position + player.MoveDirection * 5 / 4;
                    }
                    if (input.Delay >= _config.Delay / 2 && !player.Reverse)
                    {
                        player.Reverse = true;
                        player.BulletDirection = player.MoveDirection;
                        player.Target = player.Transform.position - player.MoveDirection / 4;
                    }
                }
            }
        }
    }
}
