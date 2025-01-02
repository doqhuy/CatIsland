using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public GameObject chestClosed; // Rương đóng
    public GameObject chestOpen;   // Rương mở
    public GameObject item;        // Vật phẩm trong rương
    public Transform player;       // Tham chiếu tới người chơi
    public float interactDistance = 3f; // Khoảng cách tương tác
    public float destroyDelay = 5f;     // Thời gian tự hủy sau khi mở (giây)

    private bool isChestOpened = false; // Trạng thái: đã mở rương hay chưa

    void Update()
    {
        // Kiểm tra khoảng cách và nhấn phím "J"
        if (!isChestOpened && Vector3.Distance(player.position, transform.position) <= interactDistance)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                OpenChest();
            }
        }
    }

    void OpenChest()
    {
        // Mở rương
        chestClosed.SetActive(false);
        chestOpen.SetActive(true);

        if (item != null)
        {
            item.SetActive(true); // Hiển thị vật phẩm
        }

        isChestOpened = true; // Đánh dấu rương đã mở

        // Bắt đầu quá trình tự hủy sau thời gian delay
        Invoke(nameof(DestroyChest), destroyDelay);
    }

    void DestroyChest()
    {
        // Xóa rương và vật phẩm
        if (chestOpen != null) Destroy(chestOpen);
        if (item != null) Destroy(item);
        Destroy(gameObject); // Hủy rương chính
    }
}
