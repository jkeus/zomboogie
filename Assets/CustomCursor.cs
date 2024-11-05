using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D customCursor;
    public Vector2 cursorHotspot = Vector2.zero;

    private void Start() //never got to this, but its here ready
    {
        Cursor.SetCursor(customCursor, cursorHotspot, CursorMode.Auto);
    }
}
