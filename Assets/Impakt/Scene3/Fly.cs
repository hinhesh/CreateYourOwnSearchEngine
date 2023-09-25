using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
   public float cameraSpeed = 0.1f; // Vitesse de déplacement de la caméra

    private Camera mainCamera;
    private Vector3 targetPosition;

    void Start()
    {
        mainCamera = Camera.main;
        targetPosition = transform.position;
    }
    void Update()
    {
        // Gestion du clic de souris
        if (Input.GetMouseButton(0))
        {
            Debug.Log("hola");
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Assurez-vous que le rayon touche le sol ou une surface appropriée
                targetPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
            }
                  else
            {
                // If the ray doesn't hit anything, move the camera to the ray's endpoint.
                Vector3 targetPosition = ray.GetPoint(1000); // Move to a distant point along the ray.
                transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
            }
        }

        // Déplacement lisse de la caméra vers la position cible
        
    }

}
