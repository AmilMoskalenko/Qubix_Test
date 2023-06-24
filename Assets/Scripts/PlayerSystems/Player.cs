using UnityEngine;

namespace PlayerSystems
{
    public struct Player
    {
        public Transform Transform;
        public Vector3 Target;
        public Vector3 RotateDirection;
        public Vector3 BulletDirection;
        public Vector3 MoveDirection;
        public bool Reverse;
        public Transform MeleeWeapon;
        public int EnemiesKilled;
    }
}