using Leopotam.Ecs;
using UnityEngine;

namespace BulletSystems
{
    public class BulletInitSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private SceneData _sceneData;
        private Config _config;
        
        public void Init()
        {
            for (int i = 0; i < _config.AmountPool; i++)
            {
                var bulletEntity = _ecsWorld.NewEntity();
                ref var bullet = ref bulletEntity.Get<Bullet>();
                var bulletGO = Object.Instantiate(_sceneData.BulletPrefab, _sceneData.BulletTransform);
                bullet.Transform = bulletGO.transform;
                bullet.Transform.gameObject.SetActive(false);
            }
        }
    }
}
