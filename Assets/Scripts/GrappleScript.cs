using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class GrappleScript : MonoBehaviour
{
    private static GrappleScript instance = null;

    private LineRenderer lineRenderer;
    private SpringJoint joint;

    private Vector3 grapplePoint;
    public LayerMask groundMask;
    public float maxGrappleDistance = 50f;

    public Transform grapplePosition;
    public Transform playerPosition;
    public Transform cameraPosition;

    public float jointSpring = 5f;
    public float jointDramper = 3f;
    public float jointMassScale = 3f;

    public bool isGrappling;
    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) 
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    private void LateUpdate()
    {
        DrawGrapple();  
    }

    public void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraPosition.position, cameraPosition.forward, out hit, maxGrappleDistance, groundMask))
        {
            grapplePoint = hit.point;
            joint = playerPosition.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(playerPosition.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.85f;
            joint.minDistance = distanceFromPoint * 0.3f;

            joint.spring = jointSpring;
            joint.damper = jointDramper;
            joint.massScale = jointMassScale;

            lineRenderer.positionCount = 2;

            isGrappling = true;


        }
    }

    public void StopGrapple()
    {
        lineRenderer.positionCount = 0;
        Destroy(joint);
        isGrappling = false; 
    }

    public void DrawGrapple()
    {
        if (!joint)
        {
            return;
        }

        lineRenderer.SetPosition(0, grapplePosition.position);
        lineRenderer.SetPosition(1, grapplePoint);
    }

    void Awake()
    {
        instance = this;
    }

    public static GrappleScript Instance
    {
        get
        {
            return instance;
        }
    }
}
