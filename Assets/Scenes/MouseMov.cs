using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMov : MonoBehaviour
{
    public float cursorSpeed = 100.0f;
    public Color hoverColor = Color.red;
    public Color jormalColor = Color.white;
    public LayerMask objectLayerMask;
      private bool isMouseOverObject = false;
      public SpriteRenderer sprite;
    private void Update()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Convert the screen position to world position
        mousePosition.z = 1.0f; // Set an arbitrary z-coordinate
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        // Move the cursor object to the world position smoothly
        transform.position = Vector3.Lerp(transform.position, worldPosition, Time.deltaTime * cursorSpeed);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, objectLayerMask))
        {
           // Debug.Log("hello");
           // if (hit.collider.gameObject == gameObject)
            //{Debug.Log("hello2");
                
                 
                    sprite.color = hoverColor;
                    isMouseOverObject = true;
                   Interactable interactable = hit.collider.GetComponent<Interactable>();
                   if (interactable != null)
                    interactable.Interact();
                }
            
            else
            {
                // Mouse is not over the specified object
              
                     sprite.color = jormalColor;
                    isMouseOverObject = false;
                
            }
             
      
    }
}
