using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicGridLayoutGroup : GridLayoutGroup
{
    public RectOffset paddingSize = new RectOffset();

    public bool isExtendWidth;
    public bool isExtendHeight;
    public float cellRatio = 1;
    public Vector4 paddingRatio;
    public Vector2 spacingRatio;
    public Vector2 spacingFix;
    
    private void SetDynamicGrid()
    {
        float originWidth = rectTransform.rect.width;
        float originHeight = rectTransform.rect.height;
        int childCount = rectChildren.Count;
        Vector2 newRectSize = rectTransform.rect.size;

        int rows = 0;
        int cols = 0;
        if (constraint == Constraint.FixedColumnCount)
        {
            cols = constraintCount;
            rows = Mathf.CeilToInt((float)childCount / constraintCount);
        }
        else if(constraint == Constraint.FixedRowCount)
        {
            rows = constraintCount;
            cols = Mathf.CeilToInt((float)childCount / constraintCount);

        }
        else
        {
            cols = constraintCount;
            rows = Mathf.CeilToInt((float)childCount / constraintCount);
        }
        m_Padding.top = (int)(paddingRatio.x * Screen.height + paddingSize.top);
        m_Padding.bottom = (int)(paddingRatio.y * Screen.height + paddingSize.bottom);
        m_Padding.left = (int)(paddingRatio.z * Screen.width + paddingSize.left);
        m_Padding.right = (int)(paddingRatio.w * Screen.width + paddingSize.right);
        m_Spacing.x = originWidth * spacingRatio.x + spacingFix.x;
        m_Spacing.y = originHeight * spacingRatio.y + spacingFix.y;




        float spaceW = (padding.left + padding.right) + (spacing.x * (cols - 1));
        float spaceH = (padding.top + padding.bottom) + (spacing.y * (rows - 1));
        float maxWidth = originWidth - spaceW;
        float maxHeight = originHeight - spaceH;

        if (constraint == Constraint.FixedColumnCount)
        {

            float width = maxWidth / cols;

            float height = cellRatio * width;

            if (isExtendHeight == true)
                newRectSize.y = height * rows + spaceH;
            else
                height = Mathf.Min(maxHeight / rows, height);
            cellSize = new Vector2(width, height);
        }
        else
        {
            float height = maxHeight / rows;
            float width = cellRatio * height;

            if (isExtendWidth == true)
                newRectSize.x = width * cols + spaceW;
            else
                width = Mathf.Min(maxWidth / cols, width);

            cellSize = new Vector2(width, height);

        }

        if (isExtendWidth == true)
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newRectSize.x);
        if (isExtendHeight == true)
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newRectSize.y);


    }
    public override void SetLayoutHorizontal()
    {
        SetDynamicGrid();
        base.SetLayoutHorizontal();
    }

    public override void SetLayoutVertical()
    {
        SetDynamicGrid();
        base.SetLayoutVertical();
    }

}


