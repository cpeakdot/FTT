using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public enum WorldUp
    {
        Up,
        Down,
        Right,
        Left,
    }

    public enum GetNormal
    {
        Normal,
        Reverse,
    }

    public WorldUp worldUp;
    public GetNormal normal;

    private void Update() 
    {
        var wUp = GetWorldUp();
        transform.LookAt(Camera.main.transform, wUp);
    }

    private Vector3 GetWorldUp()
    {
        switch (worldUp)
        {
            case WorldUp.Up:
                return Vector3.up;
            case WorldUp.Down:
                return Vector3.down;
            case WorldUp.Right:
                return Vector3.right;
            case WorldUp.Left:
                return Vector3.left;
            default:
                return Vector3.up;
        }
    }
}
