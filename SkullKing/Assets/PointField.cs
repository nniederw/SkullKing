using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TMP_InputField))]
public class PointField : MonoBehaviour
{
    public InputField Points;
    public InputField Tricks;
    public uint Round = 0;
    /// <summary>
    /// first arg: rounds;  second arg: points
    /// </summary>
    public void ListenToPointUpdate(Action<uint, int> action)
    {
        Points.GetComponent<TMP_InputField>()
            .onValueChanged.AddListener(i =>
            {
                try { action(Round, Convert.ToInt32(i)); }
                catch { action(Round, 0); }
            });
    }
    private void Start()
    {
        if (Points == null) throw new System.Exception($"{nameof(Points)} was null");
        if (Tricks == null) throw new System.Exception($"{nameof(Tricks)} was null");
    }
    public void ResetPoints()
    {
        Points.ResetPoints();
        Tricks.ResetPoints();
    }
}