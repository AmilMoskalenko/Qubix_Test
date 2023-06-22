using Leopotam.Ecs;
using PlayerSystems;
using UnityEngine;

namespace EnvironmentSystems
{
    public class CameraFollowSystem : IEcsRunSystem
    {
        private SceneData _sceneData;
        private Config _config;
        private EcsFilter<Player> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var player = ref _filter.Get1(i);
                var velocity = Vector3.zero;
                _sceneData.Camera.transform.position = Vector3.SmoothDamp(_sceneData.Camera.transform.position,
                    player.Transform.position + _config.CameraOffset, ref velocity, _config.CameraSmoothness);
            }
        }
    }
}
