using System.Linq;
using TMPro;
using UnityEngine;
public class PointsColumn : Column
{
    private int[] Points;
    private PointField[] PointFields;
    private PointsColumn NextColumn = null;
    private bool IsEndColumn = false;
    [SerializeField] private GameObject NameText;
    [SerializeField] private GameObject PointsField; //TODO replace with Field
    [SerializeField] private GameObject TotalField;
    private TMP_Text TotalPointsText;
    protected override GameObject FirstField => NameText;
    protected override GameObject LastField => TotalField;
    protected override void FinishSetup()
    {
        TotalPointsText = Fields.Last().GetComponentsInChildren<TMP_Text>().Single(i => i.text != "Total");
        PointFields = new PointField[Rounds];
        Points = new int[Rounds];
        for (uint i = 0; i < Rounds; i++)
        {
            PointFields[i] = Fields[i + 1].GetComponent<PointField>();
            PointFields[i].Round = i;
            PointFields[i].ListenToPointUpdate(SetPointsIn);
        }
    }
    public void SetNextColumn(PointsColumn next, bool isEnd)
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
        TotalPointsText.text = Points.Sum().ToString();
    }
    private InputField GetNextPointInputField(int row) => PointFields[row].Points;
    private InputField GetNextTrickInputField(int row) => PointFields[row].Tricks;
    void Start()
    {
        if (Fields == null) throw new System.Exception("Fields was null");
    }
    public void ResetPoints()
    {
        foreach (var pf in PointFields)
        {
            pf.ResetPoints();
        }
        Points = new int[Points.Length];
        TotalPointsText.text = Points.Sum().ToString();
    }
}