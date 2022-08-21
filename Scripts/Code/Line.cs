using UnityEngine;

public class Line : PoolableMono
{
    public LineRenderer lineRenderer;
    public Material defaultMaterial;
    public override void OnHide()
    {
        lineRenderer.loop = false;
        SetMaterial(defaultMaterial);
        SetColor(Color.white);
    }
    public void SetMaterial(Material material)
    {
        lineRenderer.material = material;
    }
    public override void OnCreate()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    public override void OnSpawn()
    {

    }
    public void SetPositions(params Vector3[] positions)
    {
        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }
    public void DrawCircle(int step, float radius)
    {
        lineRenderer.positionCount = step;
        lineRenderer.loop = true;
        var circles = radius.ToCircle(step);
        var count = 0;
        foreach (var circle in circles)
        {
            var position = circle.ToXZ();
            position.y = 0.1f;
            position += transform.position;
            lineRenderer.SetPosition(count, position);
            count++;
        }

    }
    public void SetColor(Color color)
    {
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }
    public void SetSize(float size)
    {
        lineRenderer.startWidth = size;
        lineRenderer.endWidth = size;
    }
}