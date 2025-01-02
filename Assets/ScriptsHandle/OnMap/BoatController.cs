using UnityEngine;

public class BoatController : MonoBehaviour
{
    public Transform destination; // Điểm đến mà thuyền sẽ di chuyển tới
    public float speed = 5f; // Tốc độ di chuyển của thuyền
    private bool isMoving = true; // Kiểm tra thuyền có đang di chuyển không
    private EdgeCollider2D boatCollider; // Collider của thuyền (EdgeCollider2D)
    private BoxCollider2D boatCollider1; // Collider của thuyền (EdgeCollider2D)

    void Start()
    {
        // Lấy EdgeCollider2D của thuyền
        boatCollider = GetComponent<EdgeCollider2D>();
        boatCollider1 = GetComponent<BoxCollider2D>();
        
    }

    // Di chuyển thuyền đến đích
    void Update()
    {
        if (isMoving)
        {
            MoveBoat();
        }
    }

    void MoveBoat()
    {
        // Di chuyển thuyền về đích
        float step = speed * Time.deltaTime; // Tính toán bước di chuyển
        transform.position = Vector3.MoveTowards(transform.position, destination.position, step);

        // Kiểm tra thuyền đã đến đích chưa
        if (Vector3.Distance(transform.position, destination.position) < 0.1f)
        {
            isMoving = false; // Dừng thuyền khi đến đích
            transform.position = destination.position; // Đảm bảo thuyền dừng ở đúng vị trí đích

            // Nếu thuyền có EdgeCollider2D, ẩn collider khi đến đích
            if (boatCollider != null)
            {
                boatCollider.enabled = false; // Ẩn Edgecollider của thuyền
                boatCollider1.enabled = true; // Hiện Boxcollider của thuyền
            }
        }
    }
}
