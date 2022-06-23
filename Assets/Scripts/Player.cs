using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
// ReSharper disable All

public class Player : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private GameObject tank;
	[SerializeField] private GameObject cannon;
	private float shiftSpeed;
	private float normalSpeed;
	private NavMeshAgent _navMesh;
	private float _rotate;


	private void Start()
	{
		_navMesh = GetComponentInChildren<NavMeshAgent>();
		_navMesh.enabled = true;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		shiftSpeed = speed * 2;
		normalSpeed = speed;
	}

	private void Update()
	{
		Move();
	}

	private void Move()
	{
		float vert = Input.GetAxis("Vertical");
		float hor = Input.GetAxis("Horizontal");
		transform.Rotate(0, hor, 0);
		cannon.transform.Rotate(0, -hor, 0);
		cannon.transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
		transform.Translate(Vector3.forward * vert * Time.deltaTime * speed);
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			StartCoroutine(Shift(5f));
			speed = shiftSpeed;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
			speed = normalSpeed;
	}

	private IEnumerator Shift(float time)
	{
		var buf = shiftSpeed;
		float i = 0;
		while (i < time && shiftSpeed > normalSpeed && Input.GetKey(KeyCode.LeftShift))
		{
			i += 0.01f;
			yield return new WaitForSeconds(0.01f);
			shiftSpeed /= 1.01f;
		}
		shiftSpeed = buf;
		if (Input.GetKey(KeyCode.LeftShift)) speed = normalSpeed;
	}
	
	
	
}
