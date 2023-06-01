using UnityEngine;
using TMPro;
public class RoundColumn : Column
{
    [SerializeField] protected GameObject EmptyGameObject;
    protected override GameObject FirstField => EmptyGameObject;
    protected override GameObject LastField => EmptyGameObject;
    protected override void FinishSetup()
    {
        for (int i = 1; i < Fields.Length - 1; i++)
        {
            Fields[i].GetComponentInChildren<TMP_Text>().text = $"{i}";
        }
    }
    void Start()
    {
        if (Fields == null) throw new System.Exception("Fields was null");
    }
}