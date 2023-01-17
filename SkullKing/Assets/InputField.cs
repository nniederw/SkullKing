using System.Linq;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(TMP_InputField))]
public class InputField : MonoBehaviour
{
    private static readonly char[] Numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    public InputField Next;
    private TMP_InputField ThisField;
    private void Start()
    {
        ThisField = GetComponent<TMP_InputField>();
        ThisField.onValueChanged.AddListener(ValueUpdated);
    }
    private void SelectNext()
    {
        if (Next != null)
        {
            Next.ThisField.Select();
        }
        else
        {
            Debug.Log("Next was Null");
        }
    }
    private void ValueUpdated(string s)
    {
        if (s.Length != 0 && Numbers.Contains(s.Last()))
        {
            SelectNext();
        }
    }
    public void ResetPoints()
    {
        ThisField.SetTextWithoutNotify("");
    }
}