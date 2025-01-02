using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RenameCharacterUI : MonoBehaviour
{
    public Character selectedCharacter; // Nhân vật hiện tại
    public TMP_InputField nameInputField; // InputField nhập tên mới
    public Button confirmButton; // Nút xác nhận

    private void Start()
    {
        // Gán sự kiện cho nút xác nhận
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);

        // Hiển thị tên hiện tại trong InputField
        if (selectedCharacter != null)
        {
            nameInputField.text = selectedCharacter.Name;
        }
    }

    private void OnConfirmButtonClicked()
    {
        if (!string.IsNullOrEmpty(nameInputField.text))
        {
            string oldName = selectedCharacter.Name; // Lưu tên cũ
            string newName = nameInputField.text; // Tên mới (giữ nguyên, không bị thay đổi bởi hậu tố)
            string oldFilePath = $"Assets/Resources/Save0/CharacterDefaultData/{oldName}.json";

            // Tạo tên file với hậu tố nếu bị trùng
            string newFileName = newName;
            string newFilePath = $"Assets/Resources/Save0/CharacterDefaultData/{newFileName}.json";
            int counter = 1;

            while (System.IO.File.Exists(newFilePath))
            {
                newFileName = $"{newName}_{counter}";
                newFilePath = $"Assets/Resources/Save0/CharacterDefaultData/{newFileName}.json";
                counter++;
            }

            // Cập nhật lại tên file thành tên mới (bao gồm cả hậu tố)
            string finalNameForFile = System.IO.Path.GetFileNameWithoutExtension(newFilePath);
            selectedCharacter.Name = newName; // Cập nhật tên nhân vật

            // Kiểm tra nếu file cũ tồn tại
            if (System.IO.File.Exists(oldFilePath))
            {
                // Đổi tên file
                System.IO.File.Move(oldFilePath, newFilePath);
            }
            else
            {
                // Tạo file mới nếu cần
                Debug.LogWarning($"Không tìm thấy file dữ liệu cũ: {oldFilePath}. Đang tạo file mới.");
                System.IO.File.WriteAllText(newFilePath, "{}"); // Tạo file JSON trống hoặc dữ liệu mặc định
            }

            Debug.Log($"Tên nhân vật: {newName}, Tên file: {newFileName}.json");
        }
        else
        {
            Debug.LogWarning("Tên không được để trống!");
        }
    }

}
