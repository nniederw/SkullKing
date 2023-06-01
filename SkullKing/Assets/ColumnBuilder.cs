using System;
using UnityEngine;
using System.Linq;
[RequireComponent(typeof(RectTransform))]
public class ColumnBuilder : MonoBehaviour
{
    [SerializeField] Column RoundColumn;
    [SerializeField] PointsColumn ColumnPrefab;
    [SerializeField] bool ForceUpdate = false;
    [SerializeField] uint NumberofPlayers = 3;
    [SerializeField] uint GameRounds = 10;
    private Column[] Columns = new Column[0];
    private PointsColumn[] PointsColumns = new PointsColumn[0];
    void Start()
    {
        ForceUpdate = true;
    }
    void Update()
    {
        if (ForceUpdate)
        {
            ForceUpdate = false;
            RebuildColumns();
        }
    }
    private void RebuildColumns()
    {
        //var time = DateTime.Now;
        DestroyColumns();
        // Debug.Log($"time to destroy: {(DateTime.Now-time).TotalMilliseconds} ms");
        //time = DateTime.Now;
        BuildColumns();
        //Debug.Log($"time to build: {(DateTime.Now-time).TotalMilliseconds} ms");
    }
    private void DestroyColumns()
    {
        foreach (var g in Columns)
        {
            Destroy(g.gameObject);
        }
    }
    private void BuildColumns()
    {
        Columns = new Column[NumberofPlayers + 1];
        PointsColumns = new PointsColumn[NumberofPlayers];
        Columns[0] = Instantiate(RoundColumn.gameObject, transform).GetComponent<Column>();//TODO check if .gameObject is needed
        for (int i = 0; i < NumberofPlayers; i++)
        {
            PointsColumns[i] = Instantiate(ColumnPrefab.gameObject, transform).GetComponent<PointsColumn>();
            Columns[i + 1] = PointsColumns[i];
        }
        float width = 1f / Columns.Sum(i => i.RelativeSize);
        float anchleft = 0f;

        for (int i = 0; i < Columns.Length; i++)
        {
            var rect = Columns[i].GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(anchleft, 0f);
            anchleft += width * Columns[i].RelativeSize;
            rect.anchorMax = new Vector2(anchleft, 1f);
            Columns[i].SetGameRounds(GameRounds);
        }
        for (int i = 0; i < PointsColumns.Length - 1; i++)
        {
            PointsColumns[i].SetNextColumn(PointsColumns[i + 1], false);
        }
        PointsColumns.Last().SetNextColumn(PointsColumns.First(), true);
        foreach (var col in PointsColumns)
        {
            col.SetNextSelect();
        }
    }
    public void ResetPoints()
    {
        foreach (var col in PointsColumns)
        {
            col.ResetPoints();
        }
    }
    public void SetNumberOfPlayers(string players)
    {
        try { SetNumberOfPlayers(Convert.ToUInt32(players)); }
        catch { }
    }
    public void SetNumberOfPlayers(uint players)
    {
        NumberofPlayers = players;
        RebuildColumns();
    }
}