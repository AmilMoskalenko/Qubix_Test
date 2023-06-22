using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerTransform;
    [Header("Bullet")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletTransform;
    [Header("Enemy")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private List<Transform> _enemyTransform;
    [Header("Camera")]
    [SerializeField] private Camera _camera;
    [Header("Borders")]
    [SerializeField] private List<Border> _borders;

    public GameObject PlayerPrefab => _playerPrefab;
    public Transform PlayerTransform => _playerTransform;
    public GameObject BulletPrefab => _bulletPrefab;
    public Transform BulletTransform => _bulletTransform;
    public GameObject EnemyPrefab => _enemyPrefab;
    public List<Transform> EnemyTransform => _enemyTransform;
    public Camera Camera => _camera;
    public List<Border> Borders => _borders;

    [Serializable]
    public struct Border
    {
        public Transform Transform;
        public Direction Direction;
    }
}