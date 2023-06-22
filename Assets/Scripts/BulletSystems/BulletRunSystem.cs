using Leopotam.Ecs;
using PlayerSystems;
using UnityEngine;

namespace BulletSystems
{
    public class BulletRunSystem : IEcsRunSystem
    {
        private Config _config;
        private EcsFilter<Player> _filter;
        private EcsFilter<Bullet> _filterBullet;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var player = ref _filter.Get1(i);
                if (player.BulletDirection != Vector3.zero)
                {
                    foreach (var j in _filterBullet)
                    {
                        ref var bullet = ref _filterBullet.Get1(j);
                        if (bullet.Transform.gameObject.activeSelf) continue;
                        bullet.Transform.position = player.Transform.position;
                        bullet.Transform.gameObject.SetActive(true);
                        var rb = bullet.Transform.gameObject.GetComponent<Rigidbody>();
                        rb.velocity = Vector3.zero;
                        rb.AddForce(player.BulletDirection * _config.BulletSpeed, ForceMode.Impulse);
                        break;
                    }
                    player.BulletDirection = Vector3.zero;
                }
            }
        }
    }
}
