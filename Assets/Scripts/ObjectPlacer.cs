using System;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField]
    private InputManager _inputManager;
    [SerializeField]
    private GameObject _cellIndicator;
    [SerializeField]
    private Grid _grid;
    [SerializeField]
    private GameObject _gridShader;

    private void OnEnable()
    {
        _inputManager.Move += OnMove;
        _inputManager.Discard += OnDiscard;
    }

    private void OnDisable()
    {
        _inputManager.Move -= OnMove;
        _inputManager.Discard -= OnDiscard;
    }

    private void OnDiscard()
    {
        _cellIndicator.SetActive(false);
        _gridShader.SetActive(false);
    }

    private void OnMove(Vector3 pPosition)
    {
        Vector3Int gridPos = _grid.WorldToCell(pPosition);
        _cellIndicator.transform.position = _grid.CellToWorld(gridPos);
    }
}
