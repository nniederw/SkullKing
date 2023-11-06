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
    public GameObject EmptyField;
    DisplayField[,] Elements;
    private void Start()
    {
        Debug.Log("Add Total dumbass");
        BuildTable();
    }
    private void UpdateTableElements()
    {
        for (int i = 0; i < Elements.GetLength(0); i++)
        {
            for (int j = 0; j < Elements.GetLength(1); j++)
            {
                Elements[i, j].OnLocationUpdate(i, j, Elements.GetLength(0), Elements.GetLength(1));
            }
        }
        //Parallel.ForEach(Elements.Cast<TableElement>(), (TableElement i) => i.OnLocationUpdate());
    }
    private DisplayField GetElementAt(int i, int j) => Elements[i, j];
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
        Queue<DisplayField> NameFields = new Queue<DisplayField>(Players);
        for (int i = 0; i < Players; i++)
        {
            NameFields.Enqueue(Instantiate(NameField, transform).GetComponent<DisplayField>());
            if (NameFields.Last() == null) throw new Exception($"{nameof(NameField)} seems to have no {nameof(DisplayField)} Component");
        }
        Queue<DisplayField> RoundFields = new Queue<DisplayField>(Rounds);
        for (int i = 0; i < Rounds; i++)
        {
            RoundFields.Enqueue(Instantiate(RoundField, transform).GetComponent<DisplayField>());
            if (RoundFields.Last() == null) throw new Exception($"{nameof(RoundField)} seems to have no {nameof(DisplayField)} Component");
        }
        Queue<DisplayField> PointFields = new Queue<DisplayField>(Rounds * Players);
        for (int i = 0; i < Rounds * Players; i++)
        {
            PointFields.Enqueue(Instantiate(PointField, transform).GetComponent<DisplayField>());
            if (PointFields.Last() == null) throw new Exception($"{nameof(PointField)} seems to have no {nameof(DisplayField)} Component");
        }
        Elements = new DisplayField[Rounds + 1, Players + 1];
        int length0 = Elements.GetLength(0);
        int length1 = Elements.GetLength(1);
        Elements[0, 0] = Instantiate(EmptyField, transform).GetComponent<DisplayField>();
        Elements[0, 0].Initialize(0, 0, length0, length1, GetElementAt);
        if (Elements[0, 0] == null) throw new Exception($"{nameof(EmptyField)} has no {nameof(DisplayField)}");
        for (int i = 1; i < Players + 1; i++)
        {
            Elements[0, i] = NameFields.Dequeue();
            Elements[0, i].Initialize(0, i, length0, length1, GetElementAt);
        }
        for (int i = 1; i < Rounds + 1; i++)
        {
            Elements[i, 0] = RoundFields.Dequeue();
            Elements[i, 0].Initialize(i, 0, length0, length1, GetElementAt);
        }
        for (int i = 1; i < Rounds + 1; i++)
        {
            for (int j = 1; j < Players + 1; j++)
            {
                Elements[i, j] = PointFields.Dequeue();
                Elements[i, j].Initialize(i, j, length0, length1, GetElementAt);
            }
        }
        SetElementsSize();
        UpdateTableElements();
    }
}
/*
public interface TableElement
{
    public void Initialize(int posi, int posj, Func<int, int, TableElement> getElementAt);
    public float RelativeWidth();
    public float RelativeHeight();
    public void OnLocationUpdate(int newPosi, int newPosj);
    public void SetRectAnchor(Vector2 min, Vector2 max);
}*/
public class DisplayField : MonoBehaviour
{
    protected int Posi;
    protected int Posj;
    protected int TableLength0;
    protected int TableLength1;
    protected Func<int, int, DisplayField> GetElementAt;
    private RectTransform _RectTransform;
    protected RectTransform RectTransform
    {
        get
        {
            if (_RectTransform != null) return _RectTransform;
            _RectTransform = GetComponent<RectTransform>();
            return _RectTransform;
        }
    }
    public virtual void Initialize(int posi, int posj, int length0, int length1, Func<int, int, DisplayField> getElementAt)
    {
        Posi = posi;
        Posj = posj;
        TableLength0 = length0;
        TableLength1 = length1;
        GetElementAt = getElementAt;
    }
    public virtual float RelativeWidth() => 1f;
    public virtual float RelativeHeight() => 1f;
    public virtual void OnLocationUpdate(int posi, int posj, int legth0, int length1)
    {
        Initialize(posi, posj, legth0, length1, GetElementAt);
    }
    public virtual void SetRectAnchor(Vector2 min, Vector2 max)
    {
        RectTransform.anchorMin = min;
        RectTransform.anchorMax = max;
    }
}