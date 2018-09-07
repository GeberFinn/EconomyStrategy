using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapGenerate : MonoBehaviour {

    public int width = 6;
    public int height = 6;

    public HexCell cellPrefab;

    public Text callLablePrefab;
    Canvas gridCanvas;
    HexCell[] cells;

    HexMesh hexMesh;
    private void Awake()
    {

        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();

        cells = new HexCell[height * width];

        for (int y = 0,i = 0; y< height; y++)
        {
            for (int x=0; x < width; x++)
            {
                CreateCell(x, y, i++);
            }
        }
    }

    private void Start()
    {
        hexMesh.Triangulate(cells);

    }


    void CreateCell(int x,int y, int i)
    {
        Vector3 position;
        position.x = (x+y*0.5f - y/2)*(HexMetrics.innerRadius *2f);
        position.z = y * (HexMetrics.outerRadius *1.5f);
        position.y = 0;

        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;

        Text label = Instantiate<Text>(callLablePrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        label.text = x.ToString() + "/" + y.ToString();
    }

    
}
