using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
public class Table : MonoBehaviour
{
    public static int Players = 4;
    public static int Rounds = 10;
    public GameObject PointField;
    public GameObject RoundField;
    public GameObject NameField;
    TableElement[,] Elements;
    //Dictionary<TableElement, (int i, int j)> ElementIndex = new Dictionary<TableElement, (int i, int j)>();
    private void Start()
    {
        BuildTable();
    }
    private void UpdateTableElements()
    {
        //ResetIndex();
        for (int i = 0; i < Elements.GetLength(0); i++)
        {
            for (int j = 0; j < Elements.GetLength(1); j++)
            {
                Elements[i, j].OnLocationUpdate(i, j);
            }
        }

        //Parallel.ForEach(Elements.Cast<TableElement>(), (TableElement i) => i.OnLocationUpdate());
    }
    /*private void ResetIndex()
    {
        ElementIndex.Clear();
        for (int i = 0; i < Elements.GetLength(0); i++)
        {
            for (int j = 0; j < Elements.GetLength(1); j++)
            {
                ElementIndex.Add(Elements[i, j], (i, j));
            }
        }
    }*/
    private TableElement GetElementAt(int i, int j) => Elements[i, j];
    private void SetElementsSize()
    {
        int l0 = Elements.GetLength(0);
        int l1 = Elements.GetLength(1);
        Vector2[,] anchmin = new Vector2[l0, l1];
        Vector2[,] anchmax = new Vector2[l0, l1];
        for (int i = 0; i < l0; i++) //width
        {
            float sum = 0;
            for (int j = 0; j < l1; j++)
            {
                sum += Elements[i, j].RelativeWidth();
            }
            float last = 0f;
            for (int j = 0; j < l1; j++)
            {
                anchmin[i, j] = anchmin[i, j].SetX(last);
                last += Elements[i, j].RelativeWidth() / sum;
                anchmax[i, j] = anchmax[i, j].SetX(last);
            }
        }
        for (int j = 0; j < l1; j++) //height
        {
            float sum = 0f;
            for (int i = 0; i < l0; i++)
            {
                sum += Elements[i, j].RelativeHeight();
            }
            float last = 1f; // needs to be reversed to go from top to bottom
            for (int i = 0; i < l0; i++)
            {
                anchmax[i, j] = anchmax[i, j].SetY(last);
                last -= Elements[i, j].RelativeHeight() / sum;
                anchmin[i, j] = anchmin[i, j].SetY(last);
            }
        }
        for (int i = 0; i < l0; i++)
        {
            for (int j = 0; j < l1; j++)
            {
                Elements[i, j].SetRectAnchor(anchmin[i, j], anchmax[i, j]);
            }
        }
    }
    private void BuildTable()
    {
        Queue<TableElement> RoundFields = new Queue<TableElement>(Rounds);
        for (int i = 0; i < Rounds; i++)
        {
            RoundFields.Enqueue(Instantiate(RoundField, transform).GetComponent<TableElement>());
            if (RoundFields.Last() == null) throw new Exception($"{nameof(RoundField)} seems to have no {nameof(TableElement)} Component");
        }
        Queue<TableElement> PointFields = new Queue<TableElement>(Rounds * Players);
        for (int i = 0; i < Rounds * Players; i++)
        {
            PointFields.Enqueue(Instantiate(PointField, transform).GetComponent<TableElement>());
            if (PointFields.Last() == null) throw new Exception($"{nameof(PointField)} seems to have no {nameof(TableElement)} Component");
        }
        Elements = new TableElement[Rounds, Players + 1];
        for (int i = 0; i < Rounds; i++)
        {
            Elements[i, 0] = RoundFields.Dequeue();
            Elements[i, 0].Initialize(i, 0, GetElementAt);
        }
        for (int i = 0; i < Rounds; i++)
        {
            for (int j = 1; j < Players + 1; j++)
            {
                Elements[i, j] = PointFields.Dequeue();
                Elements[i, j].Initialize(i, j, GetElementAt);
            }
        }
        SetElementsSize();
        UpdateTableElements();
    }
}
public interface TableElement
{
    public void Initialize(int posi, int posj, Func<int, int, TableElement> getElementAt);
    public float RelativeWidth();
    public float RelativeHeight();
    public void OnLocationUpdate(int newPosi, int newPosj);
    public void SetRectAnchor(Vector2 min, Vector2 max);
}