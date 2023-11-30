using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]

public class S_PlayerController : MonoBehaviour
{
    public static S_PlayerController instance;

    Rigidbody m_rigidbody = null;
    Rigidbody m_objectRigidbody = null;
    Animator m_animator = null;
    // Vector3 m_playerMoveInput  = Vector3.zero;

    [SerializeField] private S_LeftStick m_leftStick;
    [SerializeField] private float m_Acceleration;
    [SerializeField] private float m_MaxSpeed;

    private bool m_IsOnPlatform = false;

    [SerializeField] [Range(0.0f,1.0f)] private float m_decelerateRate;

    private void Awake()
    {
        if (!instance) instance = this;
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
    }

    private bool isNotInMenu = true;

    private void FixedUpdate()
    {
        Vector2 leftStick = new Vector2(m_leftStick.Horizontal, m_leftStick.Vertical );
        m_rigidbody.AddForce(leftStick.x * m_Acceleration , 0, leftStick.y * m_Acceleration , ForceMode.Acceleration) ;
        //Debug.Log(m_MaxSpeed + " | " + leftStick.x);
        //Debug.Log("Clamp X : " + (m_rigidbody.velocity.x) + " | " + (-m_MaxSpeed * Mathf.Abs(leftStick.x)) + " | " + (m_MaxSpeed * Mathf.Abs(leftStick.x)) );

        //Add speed
        Vector2 maxSpd = new Vector2(
            m_MaxSpeed * Mathf.Max(m_decelerateRate, Mathf.Abs(leftStick.x)),
            m_MaxSpeed * Mathf.Max(m_decelerateRate, Mathf.Abs(leftStick.y)));

        //Clamp speed
        m_rigidbody.velocity = new Vector3(
            Mathf.Clamp(m_rigidbody.velocity.x,-maxSpd.x, maxSpd.x), 
            m_rigidbody.velocity.y,
            Mathf.Clamp(m_rigidbody.velocity.z, -maxSpd.y, maxSpd.y));

        //Adding force if on platform
        if (m_IsOnPlatform)
        {
            //m_rigidbody.AddForce(m_objectRigidbody.velocity,ForceMode.VelocityChange);
        }

        //Rotation of the player
        if (m_leftStick.Horizontal != 0 || m_leftStick.Vertical != 0)
        {
            float yRotation = Quaternion.LookRotation(m_rigidbody.velocity).eulerAngles.y;
            transform.rotation = Quaternion.Euler(transform.rotation.x,yRotation,transform.rotation.z) ;
        }

        float t = m_rigidbody.velocity.magnitude;

        //Debug.Log(m_rigidbody.velocity.sqrMagnitude);
        m_animator.SetBool("isMoving", m_rigidbody.velocity.sqrMagnitude > 0.2f);
        // m_animator.SetFloat("Horizontal", m_leftStick.Horizontal);
        // m_animator.SetFloat("Vertical", m_leftStick.Vertical);
    }

    // private void MovePlayer()
    // {
    //     m_playerMoveInput = new Vector3(m_playerMoveInput.x * m_moveSpeed * m_rigidbody.mass, 
    //                                     m_playerMoveInput.y, 
    //                                     m_playerMoveInput.z * m_moveSpeed * m_rigidbody.mass);
    // }

    /*void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<S_MovingObject>() != null)
        {
            m_objectRigidbody = col.gameObject.GetComponent<Rigidbody>();
            m_IsOnPlatform = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.GetComponent<S_MovingObject>() != null && col.gameObject.GetComponent<Rigidbody>() == m_objectRigidbody)
        {
            m_IsOnPlatform = false;
            m_objectRigidbody = null;
        }
    }*/

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<S_MovingObject>() != null)
        {
            transform.parent = other.gameObject.transform;
            m_IsOnPlatform = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponent<S_MovingObject>() != null && col.gameObject.GetComponent<Rigidbody>() == m_objectRigidbody)
        {
            m_IsOnPlatform = false;
            transform.parent = null;
        }
    }
    public void setIsNotInMenu(bool b) { isNotInMenu = b; m_leftStick.Lock(!b); m_rigidbody.velocity = Vector3.zero; }
}
