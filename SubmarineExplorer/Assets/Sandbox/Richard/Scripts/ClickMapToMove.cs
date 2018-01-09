using UnityEngine;
using UnityEngine.AI;

// Use physics raycast hit from mouse click to set agent destination
[RequireComponent(typeof(NavMeshAgent))]
public class ClickMapToMove : MonoBehaviour
{
    NavMeshAgent m_Agent;
    RaycastHit m_HitInfo = new RaycastHit();

    [SerializeField]
    private Camera m_Camera;

    private Vector3 m_TargetLocation;

    public Transform follow;

    bool autoPilot = false;
    float currentSpeed;
    float maxSpeed = 5.0f;

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        if (m_Camera == null)
        {
            m_Camera = Camera.main;
        }

        m_Agent.speed = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
        {
            var ray = m_Camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
            {
                m_TargetLocation = m_HitInfo.point;
                autoPilot = true;
                //m_Agent.destination = m_HitInfo.point;
            }
        }

        var heading = m_TargetLocation - transform.position;
        var agentDistanceToTarget = heading.magnitude;

        // Manual piloting
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            if (autoPilot)
            {
                currentSpeed = m_Agent.velocity.magnitude;
                autoPilot = false;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(transform.up * Time.deltaTime * -25, Space.Self);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(transform.up * Time.deltaTime * 25, Space.Self);
            }
        }

        // Speed up / down
        if (Input.GetKeyDown(KeyCode.W))
        {
            m_Agent.speed += 1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            m_Agent.speed -= 1;
        }

        m_Agent.speed = Mathf.Clamp(m_Agent.speed, 0, maxSpeed);

        // If distance to target is too long for the updated navmesh, set a temporary 
        // that is facing the direction of the target.
        if (autoPilot)
        {
            m_Agent.isStopped = false;
            if (agentDistanceToTarget > 50)
            {
                var direction = heading / agentDistanceToTarget;

                float step = 0.1f * Time.deltaTime;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), step);

                m_Agent.destination = transform.position + transform.forward * 15;
            }
            else
            {
                m_Agent.destination = m_TargetLocation;
            }
        }
        else
        {
            float dt = Time.deltaTime;
            currentSpeed = currentSpeed * (1 - dt * (m_Agent.acceleration / 2)) + m_Agent.speed * (dt * (m_Agent.acceleration / 2));
            m_Agent.isStopped = true;
            m_Agent.destination = transform.position;
            m_Agent.Move(transform.forward * currentSpeed * dt);
        }


        // Transform should follow the navigation areas Y position
        Vector3 pos = transform.position;
        pos.y = follow.position.y;
        transform.position = pos;
    }
}
