using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
  public float moveSpeed = .00002f; // Vitesse de déplacement du joueur.
    public float sensitivity = 1.0f; // S
    private float rotationX = 0;
       private float rotationY = 0;

    void Update()
    {
        // Gestion de la rotation avec la souris.
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

         rotationX -= mouseY * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -45, 45); // Limitez l'angle de rotation de la caméra pour éviter le renversement.

        rotationY += mouseX * sensitivity;
 
      //  transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
        // Gestion du déplacement en avant lorsque le bouton de la souris est enfoncé.
      
    }
}
