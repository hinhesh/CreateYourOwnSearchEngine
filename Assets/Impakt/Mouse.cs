using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Texture cursorTextureNormal;
	public Texture cursorTextureNo;
	private bool opTag = false;
	private Ray ray;
	private RaycastHit hit;
void Start()
{
    SetCursorTexture(cursorTextureNormal);
}

void Update()
{
   
    if (Physics.Raycast(ray, out hit))
    {
        bool useNormalCursorTexture = hit.collider.gameObject.tag.Equals("grond");
        SetCursorTexture(useNormalCursorTexture ? cursorTextureNormal : cursorTextureNo);
    }
}

void SetCursorTexture(Texture tex)
{
  //  Cursor.SetCursor(tex, Vector2.zero, CursorMode.Auto);
}
}
