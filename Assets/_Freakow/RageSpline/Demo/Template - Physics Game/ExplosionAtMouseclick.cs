using UnityEngine;

public class ExplosionAtMouseclick : MonoBehaviour {

    private Rigidbody2D[] rigidbodies;
    public float force, radius;

	// Use this for initialization
	void Start () {
        rigidbodies = FindObjectsOfType(typeof(Rigidbody2D)) as Rigidbody2D[];
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Input.mousePosition;
			mousePos.z = transform.position.z;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            //Vector3 worldPos = Camera.main.ScreenToViewportPoint(mousePos);
            foreach (Rigidbody2D body in rigidbodies)
                body.AddExplosionForce(force, worldPos, radius);
        }
	}
}

//Copy-pasta from https://forum.unity.com/threads/need-rigidbody2d-addexplosionforce.212173/
public static class Rigidbody2DExtension
{
    public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        body.AddForce(dir.normalized * (wearoff <= 0f ? 0f : explosionForce) * wearoff);
    }
 
    public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        Vector3 baseForce = dir.normalized * (wearoff <= 0f ? 0f : explosionForce) * wearoff;
        body.AddForce(baseForce);
 
        float upliftWearoff = 1 - upliftModifier / explosionRadius;
        Vector3 upliftForce = Vector2.up * explosionForce * upliftWearoff;
        body.AddForce(upliftForce);
    }
}
