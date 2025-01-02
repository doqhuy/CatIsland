using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAutoMove2D : MonoBehaviour
{
    public Transform[] waypoints; // Danh sách các điểm để con vật di chuyển qua
    public float speed = 2f; // Tốc độ di chuyển
    public float waitTime = 2f; // Thời gian dừng lại tại mỗi điểm
    public bool randomMovement = false; // Di chuyển ngẫu nhiên hay theo thứ tự

    private int currentWaypointIndex = 0; // Chỉ số điểm hiện tại
    private bool isWaiting = false; // Trạng thái dừng lại

    void Start()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError($"No waypoints assigned for animal movement on {gameObject.name}. Please assign waypoints in the Inspector.");
            enabled = false; // Tắt script nếu không có waypoint
        }
    }

    void Update()
    {
        if (waypoints.Length > 0 && !isWaiting)
        {
            MoveToWaypoint();
        }
    }

    void MoveToWaypoint()
    {
        // Lấy vị trí của điểm hiện tại
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // Di chuyển con vật đến điểm đó
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        // Quay mặt con vật về hướng di chuyển (flip sprite)
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        if (direction.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
        }

        // Kiểm tra nếu đã đến gần điểm mục tiêu
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            StartCoroutine(WaitAtWaypoint());
        }
    }

    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        // Chọn điểm tiếp theo
        if (randomMovement)
        {
            currentWaypointIndex = Random.Range(0, waypoints.Length);
        }
        else
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        isWaiting = false;
    }
}