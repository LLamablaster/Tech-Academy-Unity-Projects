using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    // know what objects are clickable
    public LayerMask clickableLayer;

    //swap cursor on behavior
    public Texture2D pointer; //normal pointer
    public Texture2D target; //clickable pointer
    public Texture2D doorway; //doorway pointer
    public Texture2D combat; //enemy pointer

    public EventVector3 OnClickEnvironment;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 1000, clickableLayer.value))
        {
            bool door = false;
            bool item = false;

            if(hit.collider.gameObject.tag == "Doorway")
            {
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                door = true;
            }

            else if(hit.collider.gameObject.tag == "Item")
            {
                Cursor.SetCursor(combat, new Vector2(16, 16), CursorMode.Auto);
                item = true;
            }

            else
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
            }

            if(Input.GetMouseButtonDown(0))
            {
                if(door)
                {
                    Transform doorway = hit.collider.transform;
                    OnClickEnvironment.Invoke(doorway.position);
                    Debug.Log("DOOR");
                } 

                else if (item)
                {
                    Transform itemy = hit.collider.transform;
                    OnClickEnvironment.Invoke(itemy.position);
                    Debug.Log("ITEM");
                }

                else
                {
                    OnClickEnvironment.Invoke(hit.point);
                    Debug.Log(hit.point.ToString());
                }
            }
        }
        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }
    }
}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3>
{

}