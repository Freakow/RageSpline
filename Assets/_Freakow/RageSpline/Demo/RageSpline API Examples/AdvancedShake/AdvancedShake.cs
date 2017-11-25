using UnityEngine;
using System.Collections;

public class AdvancedShake : MonoBehaviour {
    
    private IRageSpline rageSpline;
    private Vector3[] originalPositions;
    private Vector3[] targetPositions;

    public Vector2 shakeSizeInNormalSpace;
    public float minFrameGap = 0.2f;
    public float maxFrameGap = 0.5f;
    public float easing = 0.1f;
    private float currentShakeGap = 0f;
    private float timeSinceLastShake = 0f;
    
    void Awake()
    {
        // Store the instance of RageSpline to avoid calling it every frame for speed/convenience.
        // Cast to IRageSpline for cleaner API access
        rageSpline = GetComponent(typeof(RageSpline)) as IRageSpline;

        // Array for movement Target positions per control point
        targetPositions = new Vector3[rageSpline.GetPointCount()];
        
        // Store the original control point positions
        originalPositions = new Vector3[rageSpline.GetPointCount()];
        
        for (int index = 0; index < rageSpline.GetPointCount(); index++)
        {
            originalPositions[index] = rageSpline.GetPosition(index);
        }
    }

    void Update()
    {
        timeSinceLastShake += Time.deltaTime;

        // Is it time for a new shake?
        if (timeSinceLastShake > currentShakeGap)
        {
            // Iterate through all the control points
            for (int index = 0; index < rageSpline.GetPointCount(); index++)
            {
                // Randomise a new shake vector
                Vector3 shakeVector = new Vector3(
                    Random.Range(-0.5f * shakeSizeInNormalSpace.x, 0.5f * shakeSizeInNormalSpace.x),
                    Random.Range(-0.5f * shakeSizeInNormalSpace.y, 0.5f * shakeSizeInNormalSpace.y),
                    0f);

                // Get normal and tangent for the control point. We will shake along the normal.
                Vector3 normal = rageSpline.GetNormal(index);
                Vector3 tangent = Vector3.Cross(normal, Camera.main.transform.forward);

                // Set a new position for the control point
                targetPositions[index] = originalPositions[index] + shakeVector.x * tangent + shakeVector.y * normal;
            }

            // When is the next new shake?
            currentShakeGap = Random.Range(minFrameGap, maxFrameGap);
            timeSinceLastShake = 0f;
        }
        else 
        {
            // Iterate through all the control points
            for (int index = 0; index < rageSpline.GetPointCount(); index++)
            {
                // Interpolate toward Target positions with Vector3.Lerp()
                Vector3 currentPosition = rageSpline.GetPosition(index);
                rageSpline.SetPoint(index, Vector3.Lerp(currentPosition, targetPositions[index], Time.deltaTime * (1f/easing)));
            }
        }

        // Finally refresh the visible mesh
        rageSpline.RefreshMesh(true, true, false);

        // Faster version (possible artifacts)
        //rageSpline.RefreshMesh(false, false, false);
    }
}
