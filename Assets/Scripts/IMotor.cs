using System;
using UnityEngine;

namespace Application
{
    public interface IMotor
    {
        void Move(Vector2 move);
    }
}
