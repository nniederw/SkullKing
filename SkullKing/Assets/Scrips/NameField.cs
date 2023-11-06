using System;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class NameField : DisplayField // TODO implement going to the next name with a point
{
    private Image Image = null;
    [SerializeField] private Color HighlightColor = Color.green;
    private Color NormalColor;
    private void Start()
    {
        Image = GetComponent<Image>();
        if (Image == null) throw new Exception($"{nameof(Image)} in {nameof(NameField)} was null, f u unity");
        NormalColor = Image.color;
    }
    public void Highlight()
    {
        Image.color = HighlightColor;
    }
    public void DeHeighlight()
    {
        Image.color = NormalColor;
    }
}