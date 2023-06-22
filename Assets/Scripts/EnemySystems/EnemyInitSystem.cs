using Leopotam.Ecs;
using UnityEngine;

namespace EnemySystems
{
    public class EnemyInitSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private SceneData _sceneData;

        public void Init()
        {
            foreach (var transform in _sceneData.EnemyTransform)
            {
                var enemyEntity = _ecsWorld.NewEntity();
                ref var enemy = ref enemyEntity.Get<Enemy>();
                var enemyGO = Object.Instantiate(_sceneData.EnemyPrefab, transform);
                enemy.Transform = enemyGO.transform;
            }
        }
    }
}
