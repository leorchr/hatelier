using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]

public class S_PlayerController : MonoBehaviour
{
    public static S_PlayerController instance;

    Rigidbody m_rigidbody = null;
    // Animator m_animator = null;
    // Vector3 m_playerMoveInput  = Vector3.zero;

    [SerializeField] private S_LeftStick m_leftStick;
    [SerializeField] private float m_Acceleration;
    [SerializeField] private float m_MaxSpeed;

    [SerializeField] [Range(0.0f,1.0f)] private float m_decelerateRate;

    private void Awake()
    {
        if (!instance) instance = this;
        m_rigidbody = GetComponent<Rigidbody>();
        // m_animator = GetComponent<Animator>();
    }

    private bool isNotInMenu = true;

    private void FixedUpdate()
    {
        Vector2 leftStick = new Vector2(m_leftStick.Horizontal, m_leftStick.Vertical );
        m_rigidbody.AddForce(leftStick.x * m_Acceleration , 0, leftStick.y * m_Acceleration , ForceMode.Acceleration) ;
        Debug.Log(m_MaxSpeed + " | " + leftStick.x);
        Debug.Log("Clamp X : " + (m_rigidbody.velocity.x) + " | " + (-m_MaxSpeed * Mathf.Abs(leftStick.x)) + " | " + (m_MaxSpeed * Mathf.Abs(leftStick.x)) );
        Vector2 maxSpd = new Vector2(
            m_MaxSpeed * Mathf.Max(m_decelerateRate, Mathf.Abs(leftStick.x)),
            m_MaxSpeed * Mathf.Max(m_decelerateRate, Mathf.Abs(leftStick.y)));

        m_rigidbody.velocity = new Vector3(
            Mathf.Clamp(m_rigidbody.velocity.x,-maxSpd.x, maxSpd.x), 
            m_rigidbody.velocity.y,
            Mathf.Clamp(m_rigidbody.velocity.z, -maxSpd.y, maxSpd.y));


        if (m_leftStick.Horizontal != 0 || m_leftStick.Vertical != 0)
        {
            float yRotation = Quaternion.LookRotation(m_rigidbody.velocity).eulerAngles.y;
            transform.rotation = Quaternion.Euler(transform.rotation.x,yRotation,transform.rotation.z) ;
        }

        // m_animator.SetFloat("Horizontal", m_leftStick.Horizontal);
        // m_animator.SetFloat("Vertical", m_leftStick.Vertical);
    }

    // private void MovePlayer()
    // {
    //     m_playerMoveInput = new Vector3(m_playerMoveInput.x * m_moveSpeed * m_rigidbody.mass, 
    //                                     m_playerMoveInput.y, 
    //                                     m_playerMoveInput.z * m_moveSpeed * m_rigidbody.mass);
    // }

    public void setIsNotInMenu(bool b) { isNotInMenu = b; m_leftStick.Lock(!b); m_rigidbody.velocity = Vector3.zero; }
}
