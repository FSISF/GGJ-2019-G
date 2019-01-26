using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject Target;
    [Range(0, 1.0f)]
    public float smoothLerp = 0.8f;
    public float offset = 0.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(Target.transform.position.x, Target.transform.position.y, transform.position.z);
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.position = Vector3.Lerp(transform.position, targetPosition - (Vector3)moveInput* offset, smoothLerp);

    }
}
