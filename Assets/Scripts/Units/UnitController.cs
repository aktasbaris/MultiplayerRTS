using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitController : MonoBehaviour
{
	[SerializeField] private UnitSelectionHandler unitSelectionHandler = null;
	[SerializeField] private LayerMask layerMask = new LayerMask();

	private Camera mainCamera = null;

	private void Start() {
		mainCamera = Camera.main;
	}

	private void Update() {
		if(!Mouse.current.rightButton.wasPressedThisFrame) return;

		Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

		if(!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) return;

		TryMove(hit.point);
	}

	private void TryMove(Vector3 point) {
		foreach(Unit unit in unitSelectionHandler.SelectedUnits) {
			unit.UnitMovement.CmdMove(point);
		}
	}
}
