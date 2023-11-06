using System;
using TMPro;
using UnityEngine;
public class PointsKeyboard : MonoBehaviour
{
    [SerializeField] private RectTransform LeftPart;
    [SerializeField] private RectTransform RightPart;
    [SerializeField] private TMP_InputField ManualInput;
    private Action<int> ReturnInput;
    public static PointsKeyboard Instance = null;
    private void Start()
    {
        if (LeftPart == null) throw new Exception($"{nameof(LeftPart)} in {nameof(PointsKeyboard)} was null");
        if (RightPart == null) throw new Exception($"{nameof(RightPart)} in {nameof(PointsKeyboard)} was null");
        if (ManualInput == null) throw new Exception($"{nameof(ManualInput)} in {nameof(PointsKeyboard)} was null");
        //ManualInput.onSelect.AddListener(i => Debug.Log("OnSelect"));
        ManualInput.keyboardType = TouchScreenKeyboardType.NumberPad;
        ManualInput.contentType = TMP_InputField.ContentType.IntegerNumber;
        ManualInput.inputType = TMP_InputField.InputType.Standard;
        ManualInput.onDeselect.AddListener(i => ManualFieldDeselect(i));
        if (Instance != null) throw new Exception($"There seem to be multiple instances of {nameof(PointsKeyboard)}");
        Instance = this;
    }
    private void Activate(bool b)
    {
        LeftPart.gameObject.SetActive(b);
        RightPart.gameObject.SetActive(b);
        ManualInput.gameObject.SetActive(b);
        ManualInput.text = "";
    }
    public void Select(int? currentValue, string InputType, Action<int> returnInput)
    {
        ReturnInput = returnInput;
        Activate(true);
        if (currentValue != null)
        {
            ManualInput.text = currentValue.ToString();
        }
        Debug.Log("//TODO InputType");//TODO InputType
    }
    public void ManualFieldDeselect(string input)
    {
        if (input != "")
        {
            InputGiven(Convert.ToInt32(input));
        }
    }
    public void InputGiven(int input)
    {
        Activate(false);
        ReturnInput?.Invoke(input);
    }
}