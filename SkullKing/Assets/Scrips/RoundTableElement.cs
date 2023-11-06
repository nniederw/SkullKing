using System;
using TMPro;
public class RoundTableElement : DisplayField
{

    private TMP_Text _TMP_Text;
    private TMP_Text TMP_Text
    {
        get
        {
            if (_TMP_Text != null) return _TMP_Text;
            _TMP_Text = GetComponentInChildren<TMP_Text>();
            return _TMP_Text;
        }
    }
    public override void Initialize(int posi, int posj, int size0, int size1, Func<int, int, DisplayField> getElementAt)
    {
        base.Initialize(posi, posj, size0, size1, getElementAt);
        SetText();
    }
    public override float RelativeWidth() => 0.35f;
    public override void OnLocationUpdate(int newPosi, int newPosj, int length0, int length1)
    {
        base.OnLocationUpdate(newPosi, newPosj, length0, length1);
        SetText();
    }
    private void SetText()
    {
        TMP_Text.text = Posi.ToString();
    }
}