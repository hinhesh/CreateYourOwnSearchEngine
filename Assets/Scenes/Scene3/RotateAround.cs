using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Transform target; // The object you want to rotate around.
    public float rotationSpeed = 30.0f; 
    private bool isRotating = false;

    void Update()
    {
        if (isRotating)
        {
            Debug.Log("holalal");
            // Calculate the rotation axis (usually up vector in Unity).
            Vector3 rotationAxis = Vector3.up;

            // Calculate the rotation angle based on the mouse input.
            float rotationAngle = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

            // Rotate the object around the target.
            transform.RotateAround(target.position, rotationAxis, rotationAngle);
        }
    }

    void OnMouseDown() 
    {
        Debug.Log("holaj");
        // Toggle rotation when the object is clicked.
        isRotating = !isRotating;
    }
}
