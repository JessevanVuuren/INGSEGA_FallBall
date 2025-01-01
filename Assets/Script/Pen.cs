using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int fill = 0;
    public Color color;
    public RectTransform fillImage;
    public PhysicsMaterial2D physicsMat;
    public int penIndex;
    public RectTransform parentPen;
    public TextMeshProUGUI textMeshProUGUI;

    private Vector3 _startPos;
    private float fullLength = 135.05755f;
    private int startAmount;
    private Vector3 _startPosPen;

    public void Start()
    {
        _startPos = fillImage.anchoredPosition;
        startAmount = fill;
        _startPosPen = parentPen.anchoredPosition;
        textMeshProUGUI.SetText(fill.ToString());
    }

    public void SetAsSelectedPen(int pen)
    {
        Vector3 pos = parentPen.anchoredPosition;
        Debug.Log(_startPosPen);
        if (pen == penIndex)
        {
            pos.y = -87 + 10;
        }
        else
        {
            pos.y = -87 - 10;
        }
        parentPen.anchoredPosition = pos;

    }

    public void Use()
    {
        if (fill > 0)
        {
            fill--;
            SetAbsPosFillImg();
        }
    }

    public void AddFill(int fill)
    {
        this.fill += fill;
        SetAbsPosFillImg();
    }

    public void SetFill(int fill)
    {
        this.fill = fill;
        SetAbsPosFillImg();
    }

    public void ResetPenFill()
    {
        this.fill = startAmount;
        SetAbsPosFillImg();
    }

    private void SetAbsPosFillImg()
    {
        textMeshProUGUI.SetText(fill.ToString());
        Vector3 pos = _startPos;
        pos.y -= fullLength / startAmount * (startAmount - fill);
        fillImage.anchoredPosition = pos;
    }
}

