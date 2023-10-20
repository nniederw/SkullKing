using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TrickColumn : Column
{
    [SerializeField] protected GameObject EmptyGameObject;
    protected override GameObject FirstField => EmptyGameObject;
    protected override GameObject LastField => EmptyGameObject;
    private TMP_Text[] Text;
    private int[] Tricks;
    private InputField [,] TricksInput;
    protected override void FinishSetup()
    {
        Text = new TMP_Text[Rounds];
        Tricks = new int[Rounds];
        //TricksInput = new List<InputField>[Rounds];
        for (int i = 1; i < Fields.Length - 1; i++)
        {
            Text[i - 1] = Fields[i].GetComponentInChildren<TMP_Text>();
        }
    }
    //public void SetTrick
    private void Start()
    {
        if (Fields != null) throw new System.Exception("Fields was already set");
        if (Text != null) throw new System.Exception("Text was already set");
        if (Tricks != null) throw new System.Exception("Tricks was already set");
    }
}