using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject player;
    public Line linePrefab;
    public Pen[] pens;
    public Transform upLeft;
    public Transform downRight;


    private Vector3 _startBallPos;
    private Rigidbody2D _rigidbodyPlayer;
    private Camera _cam;
    private float _currentScale;
    private readonly List<Line> _lines = new();
    public const float RESOLUTION = 0.01f;
    private int _selectedPen = 0;
    private Vector2 _oldMousePos;
    private Line _currentLine;
    public bool _ableToDraw = true;
    private int _fillUsed;

    void Start()
    {
        _cam = Camera.main;
        _rigidbodyPlayer = player.GetComponent<Rigidbody2D>();
        _currentScale = _rigidbodyPlayer.gravityScale;
        _rigidbodyPlayer.gravityScale = 0;
        _startBallPos = player.transform.position;
        SetPen(_selectedPen);
    }

    void Update()
    {
        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        if (_ableToDraw && pens[_selectedPen].fill > 0 &&
            !(mousePos.x < downRight.position.x && mousePos.x > upLeft.position.x &&
            mousePos.y > downRight.position.y && mousePos.y < upLeft.position.y))
        {
            if (Input.GetMouseButton(0))
            {

                if (_currentLine != null)
                {
                    _currentLine.SetPosition(mousePos, pens[_selectedPen].color);
                }
                else
                {
                    _currentLine = Instantiate(linePrefab, mousePos, Quaternion.identity);

                    GameObject lineCollider = _currentLine.transform.GetChild(0).gameObject;
                    EdgeCollider2D edgeCollider = lineCollider.GetComponent<EdgeCollider2D>();
                    edgeCollider.sharedMaterial = pens[_selectedPen].physicsMat;

                    _currentLine.lineIndex = _selectedPen;
                }
            }

            if (Input.GetMouseButtonUp(0) && _currentLine != null && _currentLine.GetPointsCount() < 2)
            {
                Destroy(_currentLine.gameObject);
            }
        }


        if (Input.GetMouseButtonUp(0) && _currentLine != null && _currentLine.GetPointsCount() >= 2)
        {
            AddLine();
        }
    }

    private void AddLine()
    {
        Debug.Log("line added");
        _currentLine.fillUsed = _fillUsed;
        _fillUsed = 0;
        _lines.Add(_currentLine);
        _currentLine = null;
    }

    void FixedUpdate()
    {
        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos != _oldMousePos && Input.GetMouseButton(0) && pens[_selectedPen].fill > 0 && _currentLine != null && _currentLine.GetPointsCount() >= 2)
        {
            pens[_selectedPen].Use();
            _fillUsed++;
            _oldMousePos = mousePos;
        }
    }

    public void StartGame()
    {
        _ableToDraw = false;
        _rigidbodyPlayer.gravityScale = _currentScale;
    }

    public void SetPen(int index)
    {
        _selectedPen = index;
        for (int i = 0; i < pens.Count(); i++)
        {
            pens[i].SetAsSelectedPen(index);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 bottomLeft = new Vector3(upLeft.position.x, downRight.position.y, 0f);
        Vector3 bottomRight = new Vector3(downRight.position.x, downRight.position.y, 0f);

        Vector3 topLeft = new Vector3(upLeft.position.x, upLeft.position.y, 0f);
        Vector3 topRight = new Vector3(downRight.position.x, upLeft.position.y, 0f);

        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
    }

    public void UndoLine()
    {
        if (_lines.Count == 0) return;

        int index = _lines.Count - 1;
        Line line = _lines[index];
        pens[line.lineIndex].AddFill(line.fillUsed);
        Destroy(line.gameObject);
        _lines.RemoveAt(index);
    }

    public void ExitLevel() {
        SceneManager.LoadScene(0);
    }

    public void Reset()
    {
        for (int i = 0; i < pens.Count(); i++)
        {
            pens[i].ResetPenFill();
        }

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Line");
        foreach (GameObject item in gameObjects)
        {
            Destroy(item);
        }

        _lines.Clear();
        _ableToDraw = true;
        _rigidbodyPlayer.gravityScale = 0;
        _rigidbodyPlayer.linearVelocity = Vector3.zero;
        _rigidbodyPlayer.angularVelocity = 0f;
        _rigidbodyPlayer.MovePosition(_startBallPos);
    }
}