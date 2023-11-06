using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class PointsInputField : DisplayField, IPointerDownHandler
{
    private PointsInputField Next = null;
    private NameField ColumnNameField = null;
    [SerializeField] private TMP_Text TricksText = null;
    [SerializeField] private TMP_Text PointsText = null;
    private bool ForceSelectTricks = false;
    private void Start()
    {
        if (TricksText == null) throw new Exception($"{nameof(TricksText)} in {nameof(PointsInputField)} was null");
        if (PointsText == null) throw new Exception($"{nameof(PointsText)} in {nameof(PointsInputField)} was null");
        //RegisterCallback<ClickEvent>(() =>;);
    }
    public override void Initialize(int posi, int posj, int length0, int length1, Func<int, int, DisplayField> getElementAt)
    {
        base.Initialize(posi, posj, length0, length1, getElementAt);
        if (TableLength1 - 1 == Posj)
        {
            Next = null;
        }
        else
        {
            var elem = GetElementAt(Posi, Posj + 1);
            if (elem is PointsInputField)
            {
                Next = (PointsInputField)elem;
            }
        }
        if (!(GetElementAt(0, Posj) is NameField)) throw new Exception($"No {nameof(NameField)} found on top of the Table");
        ColumnNameField = (NameField)GetElementAt(0, Posj);
    }
    public void SelectField(InputType type)
    {
        ColumnNameField.Highlight();
        PointsKeyboard.Instance.Select(Value(type), type.ToString(),i => //todo add information with string for InputType
        {
            switch (type)
            {
                case InputType.Tricks:
                    SetTricks(i);
                    break;
                case InputType.Points:
                    SetPoints(i);
                    break;
            }
            AfterInput(type);
        });
    }
    private int Value(InputType type)
    {
        switch (type)
        {
            case InputType.Tricks:
                return Convert.ToInt32(TricksText.text);
            case InputType.Points:
                return Convert.ToInt32(PointsText.text);
        }
        throw new Exception($"{nameof(InputType)} had an invalid value for function {nameof(Value)}");
    }
    public void AfterInput(InputType lastSelected)
    {
        ColumnNameField.DeHeighlight();
        Next?.SelectField(lastSelected);
    }

    private void SetTricks(int tricks) => TricksText.text = tricks.ToString();
    private void SetPoints(int points) => PointsText.text = points.ToString();
    public void OnPointerDown(PointerEventData eventData)
    {
        if (TricksText.text == "")
        {
            SelectField(InputType.Tricks);
        }
        else if (ForceSelectTricks)
        {
            SelectField(InputType.Tricks);
            ForceSelectTricks = false;
        }
        else
        {
            SelectField(InputType.Points);
        }
    }
    public enum InputType { Tricks, Points }
}