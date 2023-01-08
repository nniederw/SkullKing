using UnityEngine;
using TMPro;
[RequireComponent(typeof(TMP_InputField))]
public class PointField : MonoBehaviour
{
    public InputField Points;
    public InputField Tricks;
    void Start()
    {
        if (Points == null) throw new System.Exception();
        if (Tricks == null) throw new System.Exception();
    }
}