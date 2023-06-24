using System;
using InputSystems;
using Leopotam.Ecs;
using PlayerSystems;

namespace EnemySystems
{
    public class EnemyDestroySystem : IEcsRunSystem
    {
        private Config _config;
        private Network _network;
        private EcsFilter<Player, InputData> _filter;
        private EcsFilter<Enemy> _filterEnemy;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var player = ref _filter.Get1(i);
                ref var input = ref _filter.Get2(i);
                if (input.Delay == 0)
                {
                    foreach (var j in _filterEnemy)
                    {
                        ref var enemy = ref _filterEnemy.Get1(j);
                        if ((Math.Abs(player.Transform.position.z - enemy.Transform.position.z) <= 1f && 
                             Math.Abs(player.Transform.position.x - enemy.Transform.position.x) <= 0f ||
                             Math.Abs(player.Transform.position.x - enemy.Transform.position.x) <= 1f &&
                             Math.Abs(player.Transform.position.z - enemy.Transform.position.z) <= 0f) && 
                            (player.Transform.position.z > enemy.Transform.position.z && input.MoveInput == Direction.Down ||
                            player.Transform.position.z < enemy.Transform.position.z && input.MoveInput == Direction.Up ||
                            player.Transform.position.x > enemy.Transform.position.x && input.MoveInput == Direction.Left ||
                            player.Transform.position.x < enemy.Transform.position.x && input.MoveInput == Direction.Right)
                            && !enemy.Dead)
                        {
                            input.MoveLock.Add(input.MoveInput);
                            player.MeleeWeapon.gameObject.SetActive(true);
                            enemy.Transform.gameObject.SetActive(false);
                            enemy.Dead = true;
                            player.EnemiesKilled++;
                            _network.SendDataSetup(_config.KillEnemy, _network.Id, player.EnemiesKilled);
                        }
                    }
                }
                if (input.Delay >= _config.Delay && !player.Reverse)
                {
                    player.Reverse = true;
                    player.MeleeWeapon.gameObject.SetActive(false);
                    input.MoveLock.Remove(input.MoveInput);
                }
            }
        }
    }
}
