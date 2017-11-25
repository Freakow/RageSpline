using UnityEngine;

public class Drifter : MonoBehaviour {

    public float initVelocityRange = 10f;
    public float initAngularVelocityRange = 5f;

    void Start () {
        var rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(Random.Range(-initVelocityRange * 0.5f, initVelocityRange * 0.5f), Random.Range(-initVelocityRange * 0.5f, initVelocityRange * 0.5f), 0f);
        rb.angularVelocity = new Vector3(0f, 0f, Random.Range(-initAngularVelocityRange * 0.5f, initAngularVelocityRange * 0.5f));
	}

}
