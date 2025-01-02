using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D cursorTexture; // Đặt sprite con trỏ ở đây
    public Vector2 hotspot = Vector2.zero; // Điểm nhấn của con trỏ
    // Start is called before the first frame update
    void Start()
    {
        // Thay đổi con trỏ chuột
        Cursor.SetCursor(cursorTexture, hotspot, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {
        // Khôi phục con trỏ chuột về mặc định khi script bị tắt
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
    }
}
