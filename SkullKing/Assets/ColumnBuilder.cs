using UnityEngine;
[RequireComponent(typeof(RectTransform))]
public class ColumnBuilder : MonoBehaviour
{
    [SerializeField] Column ColumnPrefab;
    [SerializeField] bool ForceUpdate = false;
    [SerializeField] uint NumberofPlayers = 3;
    [SerializeField] uint GameRounds = 10;
    private Column[] Columns = new Column[0];
    void Start()
    {
        ForceUpdate = true;
    }
    void Update()
    {
        if (ForceUpdate)
        {
            ForceUpdate = false;
            DestroyColumns();
            BuildColumns();
        }
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
        Columns = new Column[NumberofPlayers];
        float width = 1f / NumberofPlayers;
        float anchleft = 0f;
        for (int i = 0; i < NumberofPlayers; i++)
        {
            Columns[i] = Instantiate(ColumnPrefab.gameObject, transform).GetComponent<Column>();
            var rect = Columns[i].GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(anchleft, 0f);
            anchleft += width;
            rect.anchorMax = new Vector2(anchleft, 1f);
            Columns[i].SetGameRounds(GameRounds);
        }
        for (int i = 0; i < NumberofPlayers - 1; i++)
        {
            Columns[i].SetNextColumn(Columns[i + 1], false);
        }
        Columns[NumberofPlayers - 1].SetNextColumn(Columns[0], true);
        foreach(var c in Columns)
        {
            c.SetNextSelect();
        }
    }
}