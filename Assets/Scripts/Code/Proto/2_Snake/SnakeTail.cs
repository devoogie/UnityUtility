using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : PoolableMono
{
    protected List<SnakeBody> bodies;
    protected List<Vector2> positions;
    
    public override void OnInitialize()
    {
        bodies = new List<SnakeBody>();
        positions = new List<Vector2>();
        
    }
    
    public override void OnDespawn()
    {
        
    }


    public override void OnSpawn()
    {

    }
    public void AddTail(SnakeBody add)
    {
        bodies.Add(add);

        bodies.Add(add);
        bodies.Sort();

    }

    void CalculatePositions()
    {
        float distance = ((Vector2)bodies[0].transform.position- positions[0]).magnitude;
        float circleDiameter = 1f;
        if (distance > circleDiameter)
        {

            Vector2 direction = ((Vector2)bodies[0].transform.position - positions[0]).normalized;
            positions.Insert(0, positions[0] + direction * circleDiameter);
            positions.RemoveAt(positions.Count - 1);
        }
    }
}