using BulletSystems;
using EnemySystems;
using EnvironmentSystems;
using InputSystems;
using Leopotam.Ecs;
using PlayerSystems;
using UnityEngine;

public class EcsStartup : MonoBehaviour
{
    [SerializeField] private SceneData _sceneData;
    [SerializeField] private Config _config;

    private EcsWorld _ecsWorld;
    private EcsSystems _systems;
    
    private void Start()
    {
        _ecsWorld = new EcsWorld();
        _systems = new EcsSystems(_ecsWorld);
    	
        _systems
            .Add(new PlayerInitSystem())
            .Add(new BulletInitSystem())
            .Add(new EnemyInitSystem())
            .Add(new CameraFollowSystem())
            .Add(new InputSystem())
            .Add(new EnemyDestroySystem())
            .Add(new PlayerTargetSystem())
            .Add(new PlayerMoveSystem())
            .Add(new BulletRunSystem())
            .Add(new BulletDestroySystem())
            .Add(new BorderSystem())
            .Inject(_sceneData)
            .Inject(_config)
            .Init();
    }
    
    private void FixedUpdate()
    {
        _systems?.Run();
    }
 
    private void OnDestroy()
    {
        _systems?.Destroy();
        _systems = null;
        _ecsWorld?.Destroy();
        _ecsWorld = null;
    }
}
