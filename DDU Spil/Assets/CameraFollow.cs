using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] public Vector3 offset;
	[SerializeField] public float damping;

	public Transform target;

	private Vector3 velocity = Vector3.zero;

	void Start()
	{

	}

	// Update is called once per frame
	void FixedUpdate()
	{
		CameraFollowPlayer();


	}
	public void SetTarget(Transform newTarget)
	{
		target = newTarget;
	}
	public void CameraFollowPlayer()
	{
		if (target)
		{
			Vector3 targetPosition = target.position + offset;
			targetPosition.z = transform.position.z;

			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, damping);
		}


	}

}
