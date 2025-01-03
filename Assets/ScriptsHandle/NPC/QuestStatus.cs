using UnityEngine;
using System.Collections;

public class QuestStatus : MonoBehaviour
{
    [SerializeField] private Quest quest; // Tham chiếu đến ScriptableObject Quest
    [SerializeField] private float delayBeforeHide = 10f; // Thời gian chờ trước khi ẩn (mặc định 10 giây)

    private bool isWaitingToHide = false; // Tránh gọi lại khi đang đợi ẩn

    private void Start()
    {
        UpdateVisibility(); // Cập nhật trạng thái ẩn/hiện khi bắt đầu
    }

    private void Update()
    {
        if (quest != null && quest.Status == "Done" && !isWaitingToHide)
        {
            StartCoroutine(HideAfterDelay());
        }
    }

    private IEnumerator HideAfterDelay()
    {
        isWaitingToHide = true; // Đánh dấu đang đợi
        yield return new WaitForSeconds(delayBeforeHide); // Đợi 10 giây (hoặc giá trị tùy chỉnh)
        gameObject.SetActive(false); // Ẩn object
    }

    private void UpdateVisibility()
    {
        if (quest != null && quest.Status != "Done")
        {
            gameObject.SetActive(true); // Hiện object nếu trạng thái không phải "Done"
            isWaitingToHide = false; // Reset trạng thái
        }
        else if (quest != null && quest.Status == "Done")
        {
            StartCoroutine(HideAfterDelay()); // Bắt đầu đếm ngược nếu trạng thái là "Done"
        }
    }
}
