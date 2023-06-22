using UnityEngine;

[CreateAssetMenu]
public class Config : ScriptableObject
{
    public float Delay;
    public float MovingSpeed;
    public float RotationSpeed;
    public float BulletSpeed;
    public float BulletTime;
    public int AmountPool;
    public Vector3 CameraOffset;
    public float CameraSmoothness;
}
