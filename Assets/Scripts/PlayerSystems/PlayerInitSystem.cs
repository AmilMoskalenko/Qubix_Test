using System.Collections.Generic;
using InputSystems;
using Leopotam.Ecs;
using UnityEngine;

namespace PlayerSystems
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private SceneData _sceneData;
        
        public void Init()
        {
            var playerEntity = _ecsWorld.NewEntity();
            ref var player = ref playerEntity.Get<Player>();
            ref var inputData = ref playerEntity.Get<InputData>();
            var playerGO = Object.Instantiate(_sceneData.PlayerPrefab, _sceneData.PlayerTransform);
            player.Transform = playerGO.transform;
            player.MeleeWeapon = playerGO.GetComponentInChildren<Collider>().transform;
            player.MeleeWeapon.gameObject.SetActive(false);
            inputData.MoveInput = Direction.None;
            inputData.MoveLock = new List<Direction>();
        }
    }
}