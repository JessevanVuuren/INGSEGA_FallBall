using UnityEngine;
using UnityEngine.PlayerLoop;

public class BallScript : MonoBehaviour
{
    private Rigidbody2D _rigid;
    public float speed_orange = 1;
    public LevelManager levelManager;
    public Transform upLeft;
    public Transform downRight;

    private Vector3 startPos;

    void Start()
    {
        _rigid = gameObject.GetComponent<Rigidbody2D>();
        startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < upLeft.position.x ||
            transform.position.x > downRight.position.x ||
            transform.position.y > upLeft.position.y ||
            transform.position.y < downRight.position.y)
        {
            Reset();
        }
    }

    private void Reset()
    {
        _rigid.gravityScale = 0;
        _rigid.linearVelocity = Vector3.zero;
        _rigid.angularVelocity = 0f;
        _rigid.MovePosition(startPos);
        levelManager._ableToDraw = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.parent != null)
        {
            Line line = collision.transform.parent.gameObject.GetComponent<Line>();
            if (line.lineIndex == 2)
            {

                Debug.Log("Adding force");
                Vector2 currentVelocity = _rigid.linearVelocity;


                Vector2 newVelocity = currentVelocity.normalized * (currentVelocity.magnitude + speed_orange);


                _rigid.linearVelocity = newVelocity;
            }
        }

        if (collision.gameObject.CompareTag("Enemy")) {
            Reset();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 bottomLeft = new Vector3(upLeft.position.x, downRight.position.y, 0f);
        Vector3 bottomRight = new Vector3(downRight.position.x, downRight.position.y, 0f);

        Vector3 topLeft = new Vector3(upLeft.position.x, upLeft.position.y, 0f);
        Vector3 topRight = new Vector3(downRight.position.x, upLeft.position.y, 0f);

        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
    }
}
