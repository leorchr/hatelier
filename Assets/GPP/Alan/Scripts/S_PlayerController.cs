using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]

public class S_PlayerController : MonoBehaviour
{
    public static S_PlayerController instance;

    Rigidbody m_rigidbody = null;
    Rigidbody m_objectRigidbody = null;
    Animator m_animator = null;

    Vector3 m_baseScale;

    [HideInInspector]
    public bool m_isPushing = false;
    [HideInInspector]
    public GameObject m_PushedObject = null;
    [HideInInspector]
    public BoxCollider m_PushCollider = null;
    // Vector3 m_playerMoveInput  = Vector3.zero;

    [SerializeField] private S_LeftStick m_leftStick;
    [SerializeField] private float m_Acceleration;
    [SerializeField] private float m_MaxSpeed;

    private bool m_IsOnPlatform = false;

    [SerializeField] [Range(0.0f,1.0f)] private float m_decelerateRate;

    [Header("Sound")]
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private AudioSource playerSource;

    private void Awake()
    {
        if (!instance) instance = this;
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        playerSource = GetComponent<AudioSource>();
        m_baseScale = transform.localScale;
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
        if ((m_leftStick.Horizontal != 0 || m_leftStick.Vertical != 0) && !m_isPushing)
        {
            float dist = 0.6f;
            Vector3 rayPos = new Vector3(transform.position.x,transform.position.y + 1,transform.position.z);
            Vector3 rayDir = new Vector3(leftStick.x, 0, leftStick.y);
            bool rayHit = Physics.Raycast(rayPos, rayDir,dist) ;
            if (m_rigidbody.velocity != Vector3.zero && !rayHit) {
                float yRotation = Quaternion.LookRotation(m_rigidbody.velocity).eulerAngles.y;
                transform.rotation = Quaternion.LookRotation(m_rigidbody.velocity);
                transform.rotation = Quaternion.Euler(transform.rotation.x, yRotation, transform.rotation.z);
            }
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
            Vector3 pScale = transform.parent.lossyScale;
            //transform.localScale = new Vector3(m_baseScale.x/pScale.x,m_baseScale.y/pScale.y,m_baseScale.z/pScale.z);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponent<S_MovingObject>() != null && col.gameObject.GetComponent<Rigidbody>() == m_objectRigidbody)
        {
            m_IsOnPlatform = false;
            transform.parent = null;

            //transform.localScale = m_baseScale;
        }
    }
    public void setIsNotInMenu(bool b) { isNotInMenu = b; m_leftStick.Lock(!b); m_rigidbody.velocity = Vector3.zero; }

    public void setDir(dir d) {
        switch (d)
        {
            case dir.Left:
            case dir.Right:
                m_leftStick.changeAxis(AxisOptions.Horizontal);
                break;
            case dir.Front:
            case dir.Back:
                m_leftStick.changeAxis(AxisOptions.Vertical);
                break;
            default:
                m_leftStick.changeAxis(AxisOptions.Both);
                break;
        }
    }

    public void createCollider(bool active, Side s) {
        if (active)
        {
            GameObject go = s.transform.parent.gameObject;
            m_PushCollider = gameObject.AddComponent<BoxCollider>();
            BoxCollider bc = go.GetComponent<BoxCollider>();
            Vector3 scale = go.transform.localScale;
            Vector3 fwd = transform.forward;
           
            
            m_PushCollider.size = new Vector3(scale.x * bc.size.x, scale.y * bc.size.y, scale.z * bc.size.z);

            float gap = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(go.transform.position.x, go.transform.position.z)) ;
            float ygap = go.transform.position.y - transform.position.y;

            m_PushCollider.center = bc.center + new Vector3(0, ygap, gap) ;
        }
        else
        {
            Destroy(m_PushCollider);
            m_PushCollider = null;
        }

    }

    public void StepSound()
    {
        int currentSound = Random.Range(0, audioClips.Count);
        playerSource.clip = audioClips[currentSound];

        playerSource.Play();
    }
}
