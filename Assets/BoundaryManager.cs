using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    public float minX, maxX, minY, maxY;

    private void Start()
    {
        //get the boundaries from the camera's inital positon since we dont move
        Camera cam = Camera.main;
        
        float verticalExtent = cam.orthographicSize;
        float horizontalExtent = verticalExtent * cam.aspect;

        minX = cam.transform.position.x - horizontalExtent;
        maxX = cam.transform.position.x + horizontalExtent;
        minY = cam.transform.position.y - verticalExtent;
        maxY = cam.transform.position.y + verticalExtent;
    }
}
