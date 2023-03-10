using System;
using System.Linq;
using TMPro;
using UnityEngine;
public class Column : MonoBehaviour
{
    private GameObject[] Fields = null;
    private int[] Points;
    private PointField[] PointFields;
    private Column NextColumn = null;
    private bool IsEndColumn = false;
    [SerializeField] private GameObject NameText;
    [SerializeField] private PointField PointsField;
    [SerializeField] private GameObject TotalField;
    private TMP_Text TotalText;
    public void SetGameRounds(uint rounds)
    {
        if (Fields != null) throw new System.Exception();
        Fields = new GameObject[rounds + 2];
        Fields[0] = Instantiate(NameText, transform);
        for (int i = 1; i < rounds + 1; i++)
        {
            Fields[i] = Instantiate(PointsField.gameObject, transform);
        }
        Fields[rounds + 1] = Instantiate(TotalField, transform);
        TotalText = Fields[rounds + 1].GetComponentsInChildren<TMP_Text>().Where(i => i.text != "Total").First();
        BuildRows();
        PointFields = new PointField[rounds];
        Points = new int[rounds];
        for (uint i = 0; i < rounds; i++)
        {
            PointFields[i] = Fields[i + 1].GetComponent<PointField>();
            PointFields[i].Round = i;
            PointFields[i].ListenToPointUpdate(SetPointsIn);
        }
    }
    public void SetNextColumn(Column next, bool isEnd)
    {
        NextColumn = next;
        IsEndColumn = isEnd;
    }
    public void SetNextSelect()
    {
        Fields[0].GetComponent<InputField>().Next = NextColumn.Fields[0].GetComponent<InputField>();
        for (int i = 0; i < PointFields.Length; i++)
        {
            if (IsEndColumn)
            {
                PointFields[i].Tricks.Next = NextColumn.GetNextPointInputField(i);
                if (i != PointFields.Length - 1)
                {
                    PointFields[i].Points.Next = NextColumn.GetNextTrickInputField(i + 1);
                }
            }
            else
            {
                PointFields[i].Tricks.Next = NextColumn.GetNextTrickInputField(i);
                PointFields[i].Points.Next = NextColumn.GetNextPointInputField(i);
            }
        }

    }
    private void SetPointsIn(uint round, int points)
    {
        Points[round] = points;
        TotalText.text = Points.Sum().ToString();
    }
    private InputField GetNextPointInputField(int row) => PointFields[row].Points;
    private InputField GetNextTrickInputField(int row) => PointFields[row].Tricks;
    private void BuildRows()
    {
        int rows = Fields.Length;
        float height = 1f / rows;
        float anchUp = 1f;
        for (int i = 0; i < rows; i++)
        {
            var rect = Fields[i].GetComponent<RectTransform>();
            rect.anchorMax = new Vector2(rect.anchorMax.x, anchUp);
            anchUp -= height;
            rect.anchorMin = new Vector2(rect.anchorMin.x, anchUp);
        }
    }
    void Start()
    {
        if (Fields == null) throw new System.Exception();
    }
    public void ResetPoints()
    {
        foreach (var pf in PointFields)
        {
            pf.ResetPoints();
        }
        Points = new int[Points.Length];
        TotalText.text = Points.Sum().ToString();
    }
}