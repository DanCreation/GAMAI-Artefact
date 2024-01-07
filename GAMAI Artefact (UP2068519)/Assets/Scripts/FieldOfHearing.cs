
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Perception))]
public class FieldOfHearing : MonoBehaviour
{
    public Vector3 playerPosition; // I added this line
    public bool heardPlayer = false; // I added this line

    public GameObject player; 
    

    public float soundRadius;

    [Range(0, 360)]
    public float soundAngle;

    public LayerMask targetLayer;

    public LayerMask obstacleLayer;

    public List<Transform> heardTargets = new List<Transform>();

    public float meshResolution;
    public int edgeResolveIterations;
    public float edgeDstThreshold;
    public MeshFilter viewMeshFilter;
    Mesh viewMesh;
    public bool drawFOH = true;

    // Start is called before the first frame update
    private void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        InvokeRepeating("FindHeardTargets", 0.2f, 0.2f);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FindHeardTargets()
    {
        heardTargets.Clear();

        Collider[] targets = Physics.OverlapSphere(transform.position, soundRadius, targetLayer);

        foreach (Collider target in targets)
        {
            Vector3 toTarget = (target.transform.position - transform.position);

            Vector3 toTargetNormalized = toTarget.normalized;

            if (Vector3.Angle(transform.forward, toTargetNormalized) < soundAngle / 2 && !Physics.Raycast(transform.position, toTargetNormalized, toTarget.magnitude, obstacleLayer) && player.GetComponent<PlayerMovement>().speed == 20)
            {
                heardTargets.Add(target.transform);
                heardPlayer = true;//////////////////////////////
            }                   //////////////////////////////
            else                // I added these lines
            {                  //////////////////////////////
                heardPlayer = false; /////////////////////////
            }                      /////////////////////////
        }

        Perception percept = GetComponent<Perception>();

        percept.ClearFoV();
        foreach (Transform target in heardTargets)
        {
            percept.AddMemory(target.gameObject);
            //seePlayer = true;
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position; // I added this line
        }
    }

    private void LateUpdate()
    {
        if (drawFOH)
        {
            viewMeshFilter.gameObject.SetActive(true);
            DrawFieldOfView();

            foreach (Transform target in heardTargets)
            {
                Debug.DrawLine(transform.position, target.position, Color.red);
            }
        }
        else
        {
            viewMeshFilter.gameObject.SetActive(false);
        }
    }

    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(soundAngle * meshResolution);
        float stepAngleSize = soundAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - soundAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            if (i > 0)
            {
                bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;

                if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;

        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();

        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;

            if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, soundRadius, obstacleLayer))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * soundRadius, soundRadius, globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}
