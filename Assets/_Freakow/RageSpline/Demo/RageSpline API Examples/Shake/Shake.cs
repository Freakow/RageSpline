using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour {

    private IRageSpline rageSpline;
    public Vector2 shakeSize;
    
	void Awake () {
        // Store the instance of RageSpline to avoid calling it every frame for speed/convenience.
        // Cast to IRageSpline for cleaner API access
        rageSpline = GetComponent(typeof(RageSpline)) as IRageSpline;
	}

	void Update () {
        // Iterate through all the control points
        for (int index = 0; index < rageSpline.GetPointCount(); index++)
        {
            // Get the current control point position in localspace coordinates
            Vector3 oldPosition = rageSpline.GetPosition(index);

            // Randomise a new shake vector
            Vector3 shakeVector = new Vector3(
                Random.Range(-0.5f * shakeSize.x, 0.5f * shakeSize.x), 
                Random.Range(-0.5f * shakeSize.y, 0.5f * shakeSize.y), 
                0f);

            // Set a new position for the control point
            rageSpline.SetPoint(index, oldPosition + shakeVector);

        }

        // Finally refresh the visible mesh
        rageSpline.RefreshMesh(true, true, true);

        // Faster version (possible artifacts)
        // rageSpline.RefreshMesh(false, false, false);
	}
}
