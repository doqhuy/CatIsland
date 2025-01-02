using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFocus : MonoBehaviour
{
	public float smoothSpeed = 0.125f;  // Điều chỉnh độ mượt của việc di chuyển camera

	void LateUpdate()
	{
		GameObject gObject = GameObject.Find("Player");
		Transform target = gObject.transform;
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);  // Giữ trục Z của camera không thay đổi
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}

