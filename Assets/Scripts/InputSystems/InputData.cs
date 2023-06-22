using System.Collections.Generic;

namespace InputSystems
{
    public struct InputData
    {
        public Direction MoveInput;
        public float Delay;
        public List<Direction> MoveLock;
    }
}

public enum Direction
{
    Up,
    Down,
    Right,
    Left,
    None
}