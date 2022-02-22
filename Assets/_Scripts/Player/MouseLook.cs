using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
	public InputActionAsset test;
	Vector2 rotation = Vector2.zero;
	public float sensitivity = 3;

	private void Update () {
		rotation.y += Input.GetAxis ("Mouse X");
		rotation.x += -Input.GetAxis ("Mouse Y");
		transform.eulerAngles = (Vector2)rotation * sensitivity;
	}
}
