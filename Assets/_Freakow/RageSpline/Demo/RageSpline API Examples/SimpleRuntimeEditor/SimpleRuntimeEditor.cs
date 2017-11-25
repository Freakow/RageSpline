using UnityEngine;
using System.Collections;

public class SimpleRuntimeEditor : MonoBehaviour {

    private RageSpline activeControl;
    private int activeControlPointIndex = -1;
    private enum ControlType { Point = 0, InCtrl, OutCtrl, NotSelected };
    private ControlType activeControlType = ControlType.NotSelected;
    private IRageSpline rageSpline;
    
    public GameObject controlPointPrefab;
    public GameObject handleControlPointPrefab;
    public GameObject handleLinePrefab;
    public Color selectedColor = new Color(1f, 1f, 0.4f);
    public Color unSelectedColor = new Color(1f, 1f, 1f);
    public float maxSelectionRange = 2f; // How far away from the mouseclick position can the control be selected
    
    private RageSpline[] controlPoints;
    private RageSpline[] inControlPoints;
    private RageSpline[] outControlPoints;
    private RageSpline[] handleLines;
    
	void Awake () {
        // Store the instance of RageSpline to avoid calling it every frame for speed/convenience.
        // Cast to IRageSpline for cleaner API access
        rageSpline = GetComponent(typeof(RageSpline)) as IRageSpline;
	}

    void Start () {
        // GUI has four types of objects. Controlpoints, in and out controls and a line between all of these.
        // All these are RageSpline objects, instantiated from a prefab
        controlPoints = new RageSpline[rageSpline.GetPointCount()];
        inControlPoints = new RageSpline[rageSpline.GetPointCount()];
        outControlPoints = new RageSpline[rageSpline.GetPointCount()];
        handleLines = new RageSpline[rageSpline.GetPointCount()];

        for (int index = 0; index < rageSpline.GetPointCount(); index++)
        {
            // Instantiate control point GUI object from the controlPointPrefab -prefab
            GameObject controlPointGO = Instantiate(
                controlPointPrefab, 
                rageSpline.GetPositionWorldSpace(index) + new Vector3(0f, 0f, -1f),
                Quaternion.identity) as GameObject;

            // Save the new RageSpline instance reference of the GUI object
            controlPoints[index] = controlPointGO.GetComponent(typeof(RageSpline)) as RageSpline;
            controlPoints[index].SetFillColor1(unSelectedColor);

            // Instantiate inCtrl handle GUI object from the handleControlPointPrefab -prefab
            GameObject inControlPointGO = Instantiate(
                handleControlPointPrefab, 
                rageSpline.GetInControlPositionWorldSpace(index) + new Vector3(0f, 0f, -1f), 
                Quaternion.identity) as GameObject;

            // Save the new RageSpline instance reference of the GUI object
            inControlPoints[index] = inControlPointGO.GetComponent(typeof(RageSpline)) as RageSpline;
            inControlPoints[index].SetFillColor1(unSelectedColor);

            // Instantiate outCtrl handle GUI object from the handleControlPointPrefab -prefab
            GameObject outControlPointGO = Instantiate(
                handleControlPointPrefab,
                rageSpline.GetOutControlPositionWorldSpace(index) + new Vector3(0f, 0f, -1f),
                Quaternion.identity) as GameObject;

            // Save the new RageSpline instance reference of the GUI object
            outControlPoints[index] = outControlPointGO.GetComponent(typeof(RageSpline)) as RageSpline;
            outControlPoints[index].SetFillColor1(unSelectedColor);

            // Instantiate line GUI object from the handleLinePrefab -prefab
            GameObject handleLineGO = Instantiate(
                handleLinePrefab,
                rageSpline.GetPositionWorldSpace(index) + new Vector3(0f, 0f, -0.5f),
                Quaternion.identity) as GameObject;

            handleLines[index] = handleLineGO.GetComponent(typeof(RageSpline)) as RageSpline;
        }

        refreshEditorGUI();
    }
	
	void Update () {
        Vector3 mousePositionWS = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePositionWS = new Vector3(mousePositionWS.x, mousePositionWS.y, 0f);
        if (Input.GetMouseButtonDown(0)) // MouseDown
        {
            selectNearestItem(mousePositionWS);
        }
        if (Input.GetMouseButton(0)) // MouseDrag
        {
            updateActiveControlPosition(mousePositionWS);
            refreshEditorGUI();
            rageSpline.RefreshMesh();
        }
	}

    // This gets all the control positions from the original rageSpline (which we are editing) and moves our GUI accordingly
    public void refreshEditorGUI()
    {
        for (int index = 0; index < rageSpline.GetPointCount(); index++)
        {
            // We are just moving the GameObject transform. No need to access the API.
            controlPoints[index].transform.position = rageSpline.GetPositionWorldSpace(index) + new Vector3(0f,0f,-1f);
            inControlPoints[index].transform.position = rageSpline.GetInControlPositionWorldSpace(index) + new Vector3(0f, 0f, -1f);
            outControlPoints[index].transform.position = rageSpline.GetOutControlPositionWorldSpace(index) + new Vector3(0f, 0f, -1f);

            // Put the object transform center where the control point is 
            handleLines[index].transform.position = rageSpline.GetPositionWorldSpace(index) + new Vector3(0f, 0f, -0.5f);
            
            // Set both ends of the line where the underlying in/out ctrlpoint handles are
            handleLines[index].SetPointWorldSpace(0, rageSpline.GetInControlPositionWorldSpace(index));
            handleLines[index].SetPointWorldSpace(1, rageSpline.GetOutControlPositionWorldSpace(index));
            
            // Handles pointing to the middle to make a straight line
            handleLines[index].SetOutControlPositionPointSpace(0, (rageSpline.GetPosition(index) - rageSpline.GetOutControlPosition(index)) * 0.25f);
            handleLines[index].SetOutControlPositionPointSpace(1, (rageSpline.GetInControlPosition(index) - rageSpline.GetPosition(index)) * 0.25f);
            
            // Since we modified the spline, we need to refresh the mesh
            handleLines[index].RefreshMesh(false, true, false);
        }
    }

    // Moves the actual point in the rageSpline we are editing (called while dragging the mouse)
    public void updateActiveControlPosition(Vector3 position)
    {
        switch (activeControlType)
        {
            case ControlType.Point:
                rageSpline.SetPointWorldSpace(activeControlPointIndex, position);
                break;
            case ControlType.InCtrl:
                rageSpline.SetInControlPositionWorldSpace(activeControlPointIndex, position);
                break;
            case ControlType.OutCtrl:
                rageSpline.SetOutControlPositionWorldSpace(activeControlPointIndex, position);
                break;
        }
    }

    public void selectNearestItem(Vector3 position)
    {
        float nearestItemDistance = 999999999f;
        RageSpline previousActiveControl = activeControl;
        
        // Iterate all the GUI controls and find out which is the closest
        for (int index = 0; index < rageSpline.GetPointCount(); index++)
        {
            if ((controlPoints[index].transform.position - position).magnitude < nearestItemDistance)
            {
                nearestItemDistance = (controlPoints[index].transform.position - position).magnitude;
                activeControlPointIndex = index;
                activeControlType = ControlType.Point;
                activeControl = controlPoints[index];
            }
            if ((inControlPoints[index].transform.position - position).magnitude < nearestItemDistance)
            {
                nearestItemDistance = (inControlPoints[index].transform.position - position).magnitude;
                activeControlPointIndex = index;
                activeControlType = ControlType.InCtrl;
                activeControl = inControlPoints[index];
            }
            if ((outControlPoints[index].transform.position - position).magnitude < nearestItemDistance)
            {
                nearestItemDistance = (outControlPoints[index].transform.position - position).magnitude;
                activeControlPointIndex = index;
                activeControlType = ControlType.OutCtrl;
                activeControl = outControlPoints[index];
            }
        }

        // Check if the closest is near enough
        if (nearestItemDistance > maxSelectionRange)
        {
            activeControl = null;
            activeControlPointIndex = -1;
            activeControlType = ControlType.NotSelected;
        }

        // Did the selected control change?
        if (previousActiveControl != activeControl)
        {
            // Reset the previously selected control to unselected color
            if(previousActiveControl!=null) {
                previousActiveControl.SetFillColor1(unSelectedColor);
                previousActiveControl.RefreshMesh();
            }
            // Fill the newly selected control with selectedColor
            if (activeControl != null)
            {
                activeControl.SetFillColor1(selectedColor);
                activeControl.RefreshMesh();
            }
        }
    }
}
