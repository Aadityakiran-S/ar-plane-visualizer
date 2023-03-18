using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneMeshManager : MonoBehaviour
{
    #region Referances

    public Material planeMaterial;
    public Texture dotPatternTexture;
    public GameObject aRSessionOrigin;
    public GameObject generatedMeshPrefab;
    public TapToPlaceObject tapToPlaceObject;

    private List<Vector3> _vertices;
    private List<Vector2> _uv;
    private List<int> _triangles;
    private GameObject _createdMeshObject;
    private Texture _currentTexture;

    [SerializeField] private int _uvScale = 1;

    #endregion

    #region Singleton
    public static PlaneMeshManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region Unity Functions

    private void Start()
    {
        _currentTexture = dotPatternTexture;
    }

    private void OnDisable()
    {
        planeMaterial.mainTexture = dotPatternTexture;
    }

    #endregion

    #region Public Functions

    public void ChangePlaneTexture(Texture texture)
    {
        planeMaterial.mainTexture = texture;
        _currentTexture = texture;
    }

    public void ToggleARPlaneDetection(bool turnOn)
    {
        var planeManager = aRSessionOrigin.GetComponent<ARPlaneManager>();

        planeManager.SetTrackablesActive(turnOn);
        //planeManager.enabled = turnOn;
        planeManager.planePrefab.SetActive(turnOn);
        tapToPlaceObject.enabled = !turnOn;
    }

    public void CreateMeshOnMarkedArea(List<GameObject> pointList)
    {
        CalculateMeshParameters(pointList);

        _createdMeshObject = Instantiate(generatedMeshPrefab/*, vertices[0], Quaternion.identity*/);

        MeshGenerator meshObject = _createdMeshObject.GetComponent<MeshGenerator>();
        meshObject.InitializeMesh(_vertices, _triangles, _uv, _currentTexture);
    }

    public void ClearAllMeshData()
    {
        Destroy(_createdMeshObject);

        _vertices.Clear();
        _triangles.Clear();
    }

    #endregion

    #region Helper Functions

    void CalculateMeshParameters(List<GameObject> pointList)
    {
        if (pointList != null && pointList.Count > 2)
        {
            //Use the data in the point list to output an int[] of triangles
            //and a Vector3[] of vertices
            _vertices = new List<Vector3>();
            _triangles = new List<int>();

            //Call function that will return vertices
            _vertices = ReturnVertices(pointList);

            //Call function that will find centroid
            Vector3 centroid = ReturnCentroid(_vertices);

            //Add the centroid to the first position of vertices
            _vertices.Insert(0, centroid);

            //Call function that will return triangles
            _triangles = ReturnTrianlges(_vertices);

            //Call function that will return uvs
            _uv = ReturnUVs(_vertices, _uvScale);
        }
        else
        {
            Debug.LogError("The pointList either null or has not enough elements to form mesh");
            return;
        }
    }

    static List<Vector3> ReturnVertices(List<GameObject> pointList)
    {
        List<Vector3> vertices = new List<Vector3>();

        foreach (GameObject obj in pointList)
        {
            vertices.Add(obj.transform.position);
        }

        return vertices;
    }

    static Vector3 ReturnCentroid(List<Vector3> points)
    {
        Vector3 centroid = Vector3.zero;

        foreach (Vector3 point in points)
        {
            centroid += point;
        }

        centroid /= (points.Count());

        return centroid;
    }

    static List<int> ReturnTrianlges(List<Vector3> points)
    {
        List<int> triangles = new List<int>();

        for (int i = 1; i < points.Count - 1; i++)
        {
            triangles.Add(0);
            triangles.Add(i);
            triangles.Add(i + 1);
        }
        triangles.Add(0);
        triangles.Add(points.Count - 1);
        triangles.Add(1);

        return triangles;
    }

    static List<Vector2> ReturnUVs(List<Vector3> pointList, int scale = 1)
    {
        List<Vector2> uvList = new List<Vector2>();

        float maxWidth = pointList[0].x;
        float maxLength = pointList[0].z;

        //Iterating through the list to find maxHeight and maxWidth
        foreach (var point in pointList)
        {
            maxWidth = Mathf.Max(point.x, maxWidth);
            maxLength = Mathf.Max(point.z, maxLength);
        }

        //Assigning the UVs with scale 
        foreach (var point in pointList)
        {
            Vector2 uv = new Vector2((point.x /*/ maxWidth*/) * scale
                , (point.z /*/ maxLength*/) * scale);
            uvList.Add(uv);
        }

        return uvList;
    }

    #endregion
}
