using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider2D;
    public int fillUsed;
    public int lineIndex;

    private readonly List<Vector2> points = new();

    void Start() {
        edgeCollider2D.transform.position -= transform.position;
    }

    public void SetPosition(Vector2 pos, Color color)
    {
        if (!CanAppend(pos)) return;

        points.Add(pos);

        lineRenderer.startColor = color;
        lineRenderer.endColor = color;

        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);

        edgeCollider2D.points = points.ToArray();
    }

    private bool CanAppend(Vector2 pos) {
        if (lineRenderer.positionCount == 0) return true;

        return Vector2.Distance(lineRenderer.GetPosition(lineRenderer.positionCount - 1), pos) > LevelManager.RESOLUTION;
    }

    public int GetPointsCount() {
        return points.Count;
    }
}
