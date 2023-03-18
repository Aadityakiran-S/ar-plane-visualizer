using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class GamePieceManipulator : MonoBehaviour
{
    #region Referances
    [SerializeField]
    private List<GameObject> _spawnedPositionMarkerObjects;
    #endregion

    #region Singleton
    public static GamePieceManipulator Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region Start and Update

    private void Start()
    {
        _spawnedPositionMarkerObjects = new List<GameObject>();
    }

    #endregion

    #region Public Functions

    //To call outside
    public void AddToList(GameObject obj)
    {
        _spawnedPositionMarkerObjects.Add(obj);
    }

    public void ClearAllObjects()
    {
        foreach (GameObject obj in _spawnedPositionMarkerObjects)
            Destroy(obj);

        _spawnedPositionMarkerObjects.Clear();
    }

    public void UpdateGamePieceLineRender()
    {
        //If only one object is spawned, setting the line render to itslef
        if (_spawnedPositionMarkerObjects.Count == 1)
        {
            LineRenderer ln = _spawnedPositionMarkerObjects[0].GetComponent<LineRenderer>();

            ln.SetPosition(0, _spawnedPositionMarkerObjects[0].transform.position);
            ln.SetPosition(1, _spawnedPositionMarkerObjects[0].transform.position);
        }
        else if (_spawnedPositionMarkerObjects.Count > 1)
        {
            SetLineRenderPosition(_spawnedPositionMarkerObjects[_spawnedPositionMarkerObjects.Count - 2],
                _spawnedPositionMarkerObjects[_spawnedPositionMarkerObjects.Count - 1].transform);
        }
    }

    //Setting the line render of the last object in the list to the first object to close the loop
    public void CloseLoop()
    {
        if (_spawnedPositionMarkerObjects.Count != 0)
        {
            SetLineRenderPosition(_spawnedPositionMarkerObjects[_spawnedPositionMarkerObjects.Count - 1],
            _spawnedPositionMarkerObjects[0].transform);
        }
        else
            return;
    }

    public Vector2[] Return_PositionMarkerPositionArray()
    {
        List<Vector2> positionList = new List<Vector2>();

        if (_spawnedPositionMarkerObjects.Count != 0)
        {
            foreach (GameObject obj in _spawnedPositionMarkerObjects)
            {
                Vector3 objPosition = obj.transform.position;
                positionList.Add(new Vector2(objPosition.x, objPosition.z));

                Debug.Log("objPosition: " + objPosition);
            }
        }

        return positionList.ToArray();
    }

    public List<GameObject> Return_PositionMarkerGameObjectList()
    {
        return _spawnedPositionMarkerObjects;
    }

    #endregion

    #region Private Functions

    //Set line render from the given object to a target position
    void SetLineRenderPosition(GameObject obj, Transform targetTransform)
    {
        LineRenderer ln = obj.GetComponent<LineRenderer>();

        ln.SetPosition(0, obj.transform.position);
        ln.SetPosition(1, targetTransform.position);
    }    

    #endregion


}

