using UnityEngine;

public class PointSpawner : MonoBehaviour
{

    void Start()
    {
        CreatePoints();
    }
    void CreatePoints()
    {
        Point point1 = new Point(1, 1);
        Point point2 = new Point(2, 2);
        
        float distance = point1.DistanceTo(point2);
        Debug.Log("Расстояние между точками : " + distance);
    }
}
