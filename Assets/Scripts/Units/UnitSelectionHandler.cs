using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSelectionHandler : MonoBehaviour
{
	[SerializeField] private LayerMask layerMask = new LayerMask();

	private Camera mainCamera;
	private List<Unit> selectedUnits = new List<Unit>();
	public List<Unit> SelectedUnits { get { return selectedUnits; } }

	private void Start() {
		mainCamera = Camera.main;
	}

	private void Update() {
		if(Mouse.current.leftButton.wasPressedThisFrame) {
			ClearSelectedUnits();
			// StartSelectionArea();
		}
		else if(Mouse.current.leftButton.wasReleasedThisFrame) {
			ClearSelectionArea();
		}
	}

	private void ClearSelectedUnits() {
		foreach(Unit selectedUnit in selectedUnits) {
			selectedUnit.Deselect();
		}

		selectedUnits.Clear();
	}

	private void ClearSelectionArea() {
		Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

		if(!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) return;
		if(!hit.collider.TryGetComponent<Unit>(out Unit unit)) return;
		if(!unit.hasAuthority) return;

		selectedUnits.Add(unit);

		foreach(Unit selectedUnit in selectedUnits) {
			selectedUnit.Select();
		}
	}
}
