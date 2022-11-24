using UnityEngine;

public class IdentifySideOnPlane : MonoBehaviour
{
    [SerializeField] PlaneCreator[] planesCreator;
    [SerializeField] Mesh _mesh;
    [SerializeField] Vector3[] vertices;
    GameObject myObject;
    Material _renderMat;
    Vector3[] objectPosition;
    private int Color1 = Shader.PropertyToID("_Color");

    public void Start()
    {
        myObject = this.gameObject;
        _mesh = myObject.GetComponent<MeshFilter>().mesh;
        objectPosition = new Vector3[_mesh.vertices.Length];
    }

    void Update()
    {
        vertices = _mesh.vertices;
        _renderMat = GetComponent<MeshRenderer>().material;
        _mesh = myObject.GetComponent<MeshFilter>().mesh;

        for (var i = 0; i < vertices.Length; i++)
        {
            objectPosition[i] = transform.TransformPoint(vertices[i]);
        }

        foreach (var t in planesCreator)
        {
            int counter = 0;
            foreach (var t1 in objectPosition)
            {
                if (!t.Plane.GetSide(t1))
                {
                    counter++;
                }
            }

            if (counter >= _mesh.vertices.Length)
            {
                _renderMat.SetColor(Color1, Color.red);
                break;
            }
            _renderMat.SetColor(Color1, Color.green);
        }
    }
}