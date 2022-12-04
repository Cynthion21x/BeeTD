using UnityEngine;
using UnityEngine.UI;
     
public class AdvancedGridLayoutGroupV : GridLayoutGroup {

    [SerializeField] protected int cellsPerLine = 1;
    [SerializeField] protected float aspectRatio = 1;
     
    public override void SetLayoutVertical() {

        float height = (this.GetComponent<RectTransform>()).rect.height;

        float useableHeight = height - this.padding.vertical - (this.cellsPerLine - 1) * this.spacing.x;
        float cellHeight = useableHeight / cellsPerLine;

        this.cellSize = new Vector2(cellHeight * this.aspectRatio, cellHeight * this.aspectRatio);

        base.SetLayoutVertical();
    }

}