using UnityEngine;
public class Column : MonoBehaviour
{
    protected GameObject[] Fields = null;
    [SerializeField] private GameObject Field;
    //[SerializeField] protected GameObject EmptyGameObject;
    [SerializeField] public float RelativeSize = 1f;
    protected int Rounds => Fields.Length - 2;
    protected virtual GameObject FirstField => Field;
    protected virtual GameObject LastField => Field;
    protected virtual void FinishSetup() { }
    public void SetGameRounds(uint rounds)
    {
        if (Fields != null) throw new System.Exception("Fields was already set");
        Fields = new GameObject[rounds + 2];
        Fields[0] = Instantiate(FirstField, transform);
        for (int i = 1; i < rounds + 1; i++)
        {
            Fields[i] = Instantiate(Field.gameObject, transform);
        }
        Fields[rounds + 1] = Instantiate(LastField, transform);

        BuildRows();
        FinishSetup();
    }
    private void BuildRows()
    {
        int rows = Fields.Length;
        float height = 1f / rows;
        float anchUp = 1f;
        for (int i = 0; i < rows; i++)
        {
            var rect = Fields[i].GetComponent<RectTransform>();
            rect.anchorMax = new Vector2(rect.anchorMax.x, anchUp);
            anchUp -= height;
            rect.anchorMin = new Vector2(rect.anchorMin.x, anchUp);
        }
    }
    void Start()
    {
        if (Fields == null) throw new System.Exception("Fields was null");
    }
}