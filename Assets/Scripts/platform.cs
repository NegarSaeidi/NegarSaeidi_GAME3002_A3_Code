//#define CALC_SPRING_COEFF

using UnityEngine;

public class platform : MonoBehaviour
{
    [SerializeField]
    public float m_fSpringConstant;
    [SerializeField]
    private float m_fDampingConstant;
    [SerializeField]
    private Vector3 m_vRestPos;
    [SerializeField]
    private float m_fMass;
    [SerializeField]
    private Rigidbody m_attachedBody = null;
    [SerializeField]
    private bool m_bIsBungee = false;

    private Vector3 m_vForce;
    private Vector3 m_vPrevVel;

    private void Start()
    {
        m_fMass = m_attachedBody.mass;

        // If you want to test the CoilSpring, or Bridge scenes...
        // ...remember to remove the comment for CALC_SPRING_COEFF
#if CALC_SPRING_COEFF
        m_fSpringConstant = CalculateSpringConstant();
#endif
    }

    private void FixedUpdate()
    {
        UpdateSpringForce();
    }

    private float CalculateSpringConstant()
    {
        // k = F / dX
        // F = m * a
        // k = m * a / (xf - xi)

        float fDX = (m_vRestPos - m_attachedBody.transform.position).magnitude;

        if (fDX <= 0f)
        {
            return Mathf.Epsilon;
        }

        return (m_fMass * Physics.gravity.y) / (fDX);
    }

    private void UpdateSpringForce()
    {
        // F = -kx
        // F = -kx -bv

        if (m_bIsBungee)
        {
            float fLen = (m_vRestPos - m_attachedBody.transform.position).magnitude;

            if (fLen <= m_vRestPos.y)
            {
                return;
            }
        }

        m_vForce = -m_fSpringConstant * (m_vRestPos - m_attachedBody.transform.position) -
            m_fDampingConstant * (m_attachedBody.velocity - m_vPrevVel);

        m_attachedBody.AddForce(m_vForce, ForceMode.Acceleration);

        m_vPrevVel = m_attachedBody.velocity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(m_vRestPos, 1f);

        if (m_attachedBody)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(m_attachedBody.transform.position, 1f);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, m_attachedBody.transform.position);
        }
    }
}
