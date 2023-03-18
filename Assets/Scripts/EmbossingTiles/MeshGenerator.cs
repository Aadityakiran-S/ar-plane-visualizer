using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    #region Referances

    private Mesh _mesh;
    private Texture _texture;
    private Vector3[] _vertices;
    private int[] _triangles;
    private Vector2[] _uv;

    #endregion

    #region Start and Update

    private void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;

        RenderMesh();
    }

    #endregion

    public void InitializeMesh(List<Vector3> vertices, List<int> triangles, List<Vector2> uv, 
        Texture texture)
    {
        _vertices = vertices.ToArray();
        _triangles = triangles.ToArray();
        _uv = uv.ToArray();
        _texture = texture;
    }

    private void RenderMesh()
    {
        _mesh.Clear();

        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        _mesh.uv = _uv;

        _mesh.RecalculateNormals();

        GetComponent<MeshRenderer>().material.mainTexture = _texture;
    }
}
