using EnemySystems;
using Leopotam.Ecs;
using UnityEngine;

namespace BulletSystems
{
    public class BulletDestroySystem : IEcsRunSystem
    {
        private Config _config;
        private EcsFilter<Bullet> _filter;
        private EcsFilter<Enemy> _filterEnemy;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var bullet = ref _filter.Get1(i);
                if (bullet.Transform.gameObject.activeSelf)
                {
                    bullet.Time += Time.fixedDeltaTime;
                    foreach (var j in _filterEnemy)
                    {
                        ref var enemy = ref _filterEnemy.Get1(j);
                        if (bullet.Transform.position == enemy.Transform.position)
                        {
                            bullet.Transform.gameObject.SetActive(false);
                            enemy.Transform.gameObject.SetActive(false);
                            enemy.Dead = true;
                        }
                    }
                }
                if (bullet.Time >= _config.BulletTime)
                {
                    bullet.Time = 0;
                    bullet.Transform.gameObject.SetActive(false);
                }
            }
        }
    }
}
