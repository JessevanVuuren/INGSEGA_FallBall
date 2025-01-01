using UnityEngine;

public class MovePlatfrom : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 3f;
    public bool isVertical = false; // Boolean to toggle vertical movement

    private Vector3 _startPosition;
    private bool _movingPositive = true; // Handles direction for both horizontal and vertical movement

    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        if (isVertical)
        {
            // Vertical movement
            if (_movingPositive)
            {
                transform.position += Vector3.up * speed * Time.deltaTime;
                if (transform.position.y >= _startPosition.y + moveDistance) _movingPositive = false;
            }
            else
            {
                transform.position += Vector3.down * speed * Time.deltaTime;
                if (transform.position.y <= _startPosition.y - moveDistance) _movingPositive = true;
            }
        }
        else
        {
            // Horizontal movement
            if (_movingPositive)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
                if (transform.position.x >= _startPosition.x + moveDistance) _movingPositive = false;
            }
            else
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
                if (transform.position.x <= _startPosition.x - moveDistance) _movingPositive = true;
            }
        }
    }

    void OnDrawGizmos()
    {
        Vector3 from = new();
        Vector3 to = new();

        if (Application.isPlaying)
        {
            from = _startPosition;
            to = _startPosition;
        }
        else
        {
            from = transform.position;
            to = transform.position;
        }
        
        Gizmos.color = Color.yellow;

        if (isVertical)
        {
            from.y -= moveDistance / 2;
            to.y += moveDistance / 2;
        }
        else
        {
            from.x -= moveDistance / 2;
            to.x += moveDistance / 2;
        }

        Gizmos.DrawLine(from, to);
    }
}
