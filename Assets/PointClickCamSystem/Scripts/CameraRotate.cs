using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{

    private float rotationSpeed = 5.0f;
    private float maxYRotation = 30.0f;
    private float minYRotation = -30.0f;

    private float maxX = 30.0f;
    private float minX = -30.0f;

    private float currentYRotation = 0.0f;

    private void Start()
    {
        SetCurrentRotation();
    }

    public void SetCurrentRotation()
    {
        float mouseX = transform.rotation.eulerAngles.y;

        minX = mouseX - 30.0f;
        maxX = mouseX + 30.0f;
    }

    void Update()
    {
        if (PointClickManager.instance.FreeLookActive)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = 0.0f; 

            currentYRotation += mouseY * rotationSpeed;
            currentYRotation = Mathf.Clamp(currentYRotation, minYRotation, maxYRotation);

            float newY = transform.rotation.eulerAngles.y + mouseX * rotationSpeed;

            newY = Mathf.Clamp(newY, minX, maxX);

            transform.rotation = Quaternion.Euler(currentYRotation, newY, 0.0f);

        }
    }

}
