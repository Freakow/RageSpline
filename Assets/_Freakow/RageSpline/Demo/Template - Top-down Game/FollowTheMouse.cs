using UnityEngine;
using System.Collections;

public class FollowTheMouse : MonoBehaviour {

    public float maxSpeed;
    public float moveSpeed;
    public float turnSpeed;
    public float origParticleMinSize;
    public float origParticleMaxSize;
    public float origParticleLocalYSpeed;
    public ParticleEmitter emitter;

	// Use this for initialization
	void Start () {
        origParticleMinSize = emitter.minSize;
        origParticleMaxSize = emitter.maxSize;
        origParticleLocalYSpeed = emitter.localVelocity.y;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 1.0f;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 dirVec = worldPos - transform.position;

        if (dirVec.magnitude > 1f)
        {
            GetComponent<Rigidbody>().velocity += dirVec * Time.deltaTime * moveSpeed;
            if (Mathf.Abs(Vector3.Dot(transform.right * -1f, dirVec.normalized)) > 0.01f)
            {
                GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, Vector3.Dot(transform.right*-1f, dirVec.normalized) * turnSpeed);
            }
        }

        if (GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
        {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
        }

        emitter.minSize = origParticleMinSize * (GetComponent<Rigidbody>().velocity.magnitude / maxSpeed);
        emitter.maxSize = origParticleMaxSize * (GetComponent<Rigidbody>().velocity.magnitude / maxSpeed);
        emitter.localVelocity = new Vector3(0f, origParticleLocalYSpeed * (GetComponent<Rigidbody>().velocity.magnitude / maxSpeed), 0f);
	}
}
