using UnityEngine;

public class CircleRenderer : PoolableMono
{
    public LineRenderer lineRenderer;

    public override void OnDespawn()
    {

    }

    public override void OnInitialize()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    public override void OnSpawn()
    {

    }
    public void DrawLine(int step, float radius)
    {
        lineRenderer.positionCount = step;

        for (int i = 0; i < step; i++)
        {
            float ratio = (float)i / (float)step;
            float currentRadian = ratio * Mathf.PI * 2f;

            float x = Mathf.Cos(currentRadian) * radius;
            float y = Mathf.Sin(currentRadian) * radius;

            Vector3 currentPosition = new Vector3(x, 0.1f, y);

            lineRenderer.SetPosition(i, currentPosition);
        }
    }
    public void SetColor(Color color)
    {
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }
}