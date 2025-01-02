
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform player; // Tham chiếu đến người chơi.
    public float RadiusActive = 5f; // Bán kính kích hoạt tương tác.
    public TalkingAndQuest talkingScript; // Tham chiếu đến TalkingAndQuest script.
    public Transform[] waypoints; // Các điểm waypoint.
    public float speed = 2.0f; // Tốc độ di chuyển.
    public float waypointThreshold = 0.1f; // Khoảng cách để coi như đã đến waypoint.

    private int currentWaypointIndex = 0; // Chỉ số waypoint hiện tại.
    private bool isStopped = false; // Trạng thái NPC.
    private Animator animator; // Animator để đồng bộ hóa animation.
    private Vector2 lastDirection; // Lưu hướng di chuyển trước đó.

    void Start()
    {
        animator = GetComponent<Animator>();

        // Kiểm tra nếu không có waypoint.
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints assigned!");
        }
    }

    void Update()
    {
        // Xử lý khoảng cách đến người chơi.
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Nếu người chơi nhấn phím J và trong bán kính tương tác.
        if (distanceToPlayer < RadiusActive && Input.GetKeyDown(KeyCode.J) )
        {
            StopNPC(); // Dừng NPC.

            if (talkingScript != null)
            {
                talkingScript.ShowTalk(); // Hiển thị hội thoại.
            }
        }

        // Nếu hội thoại đã kết thúc và NPC đang dừng, tiếp tục di chuyển.
        if (talkingScript != null && !talkingScript.TalkingUI.activeSelf && isStopped)
        {
            ResumeNPC();
        }

        // Di chuyển giữa các waypoint nếu không bị dừng.
        if (!isStopped && waypoints.Length > 0)
        {
            MoveToWaypoint();
        }
    }

    void MoveToWaypoint()
    {
        // Lấy vị trí hiện tại và waypoint tiếp theo.
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = waypoints[currentWaypointIndex].position;

        // Tính toán hướng di chuyển.
        Vector2 direction = (targetPosition - currentPosition).normalized;

        // Kiểm tra nếu đã đến waypoint.
        if (Vector2.Distance(currentPosition, targetPosition) > waypointThreshold)
        {
            // Di chuyển NPC.
            transform.Translate(direction * speed * Time.deltaTime);

            // Cập nhật hướng di chuyển cho animation.
            if (animator != null)
            {
                animator.SetFloat("MoveX", direction.x);
                animator.SetFloat("MoveY", direction.y);
                animator.SetBool("IsMoving", true);
            }

            // Lưu lại hướng cuối cùng.
            lastDirection = direction;
        }
        else
        {
            // Đến waypoint, chuyển sang waypoint tiếp theo.
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

            // NPC dừng lại khi đến waypoint.
            if (animator != null)
            {
                animator.SetBool("IsMoving", false);

                // Đặt hướng animation cuối cùng để đảm bảo NPC quay mặt đúng hướng.
                animator.SetFloat("MoveX", lastDirection.x);
                animator.SetFloat("MoveY", lastDirection.y);
            }
        }
    }

    void StopNPC()
    {
        isStopped = true;
        if (animator != null)
        {
            animator.SetBool("IsMoving", false);
        }
    }

    void ResumeNPC()
    {
        isStopped = false;
    }
}



