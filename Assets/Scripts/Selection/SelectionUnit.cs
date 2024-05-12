using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class use for selecting unit (Building not include) 3 style such as 
/// 1. Left mouse button click
/// 2. drag for multiple selection
/// 3. Left shift + Left mouse button click for multiple selection
/// *** attach this scrpit in EventSystem in hierachy ***
/// </summary>
public class SelectionUnit : MonoBehaviour 
{
    public List<GameObject> selectedObject = new List<GameObject>(); // list to store selected object
    private bool isSelecting = false;
    public Vector2 startScreenPosition;
    public Vector2 endScreenPosition;
    private Rect selectionRect;
    public Color selectionBoxColor = new Color(0.5f,0.5f,0.5f,0.5f); // Color of selection box
    public Texture2D selectionTexture;
    LayerMask layerMask;

    private void Awake()
    {
       layerMask = LayerMask.GetMask("NPC");
    }
    private void Update()
    {
        //1. if click only left mouse button
        // select only single units
        if (Input.GetMouseButtonDown(0))
        {
            startScreenPosition = Input.mousePosition;
            selectedObject.Clear(); // clear previous selection
        }

        //2. if hold click and drag
        // select all unit is in the selection box
        if (Input.GetMouseButton(0))
        {
            endScreenPosition = Input.mousePosition;
            selectionRect = new Rect(
                                      Mathf.Min(startScreenPosition.x, endScreenPosition.x),
                                      Mathf.Min(Screen.height - startScreenPosition.y, Screen.height - endScreenPosition.y),
                                      Mathf.Abs(startScreenPosition.x - endScreenPosition.x),
                                      Mathf.Abs(startScreenPosition.y - endScreenPosition.y)
                                    );
            isSelecting = true;
        }
        
        //3. if Left shift + Left mouse button click
        // can select multiple unit 
        if(Input.GetMouseButtonUp(0))
        {
            SelectedObjectInBox();
            isSelecting = false;
        }
    }

    private void SelectedObjectInBox()
    {
        Ray ray = Camera.main.ScreenPointToRay(selectionRect.center);

        RaycastHit[] hits = Physics.RaycastAll(ray,
                                              float.MaxValue,
                                               layerMask);

        Debug.DrawRay(ray.origin,
                      ray.direction * (Vector3.Distance(Camera.main.transform.position, transform.position) * 2f),
                      Color.red,
                      10.0f);

        foreach (var hit in hits)
        {
            GameObject selectedGameObject = hit.collider.gameObject;
            // You can add additional checks or filters here if needed.
            selectedObject.Add(selectedGameObject);
        }

        foreach (var selectedGameObject in selectedObject)
        {
            Debug.Log("Selected : " + selectedGameObject.name);
        }
        
    }

    private void OnGUI() // draw box
    {
        if (isSelecting == true)
        {
            GUI.color = selectionBoxColor;
            GUI.DrawTexture(selectionRect,Texture2D.whiteTexture);
        }
    }

}
