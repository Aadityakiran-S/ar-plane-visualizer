using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSpawner : MonoBehaviour
{
    public LayerMask clickMask;
    public GameObject gamePiece;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = -Vector3.one;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, clickMask))
            {
                clickPosition = hit.point;

                SpawanObject(gamePiece, clickPosition);
            }
        }
    }

    public void SpawanObject(GameObject objectToPlace, Vector3 position)
    {
        GameObject currentlySpawnedObject = Instantiate(objectToPlace,
               position, Quaternion.identity);

        GamePieceManipulator.Instance.AddToList(currentlySpawnedObject);
        GamePieceManipulator.Instance.UpdateGamePieceLineRender();
    }

    public void ClearObjects()
    {
        GamePieceManipulator.Instance.ClearAllObjects();
    }

    public void CloseLoop()
    {
        GamePieceManipulator.Instance.CloseLoop();
        PlaneMeshManager.Instance.CreateMeshOnMarkedArea(
            GamePieceManipulator.Instance.Return_PositionMarkerGameObjectList());
    }
}
