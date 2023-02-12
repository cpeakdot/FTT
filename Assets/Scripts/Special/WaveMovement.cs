using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    public enum Direction
    {
        Horizontal,
        Vertical,
        Both,
    }

    public Direction direction;
    public float frequency = 1f;
    public float amplitude = 1f;
    public bool relative = false;

    private Vector3 startPosition;

    private void Awake() 
    {
        startPosition = transform.position;
    }

    public void SetStartPosition(Vector3 position)
    {
        startPosition = position;
    }

    public void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        if(direction == Direction.Horizontal || direction == Direction.Both)
        {
            x = Mathf.Sin(Time.time * frequency) * amplitude;

            if(relative)
            {
                x += startPosition.x;
            }
        }

        if(direction == Direction.Vertical || direction == Direction.Both)
        {
            y = Mathf.Sin(Time.time * frequency) * amplitude;

            if(relative)
            {
                y += startPosition.y;
            }
        }
        Vector3 targetPosition = new Vector3(x, y, z);

        transform.position = targetPosition;
    }
}
