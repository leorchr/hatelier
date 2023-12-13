using System;
using UnityEngine;
using UnityEngine.VFX;

public class S_Player_Anim_Manager : MonoBehaviour
{
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;

    public float minSpeedToWalk = 0.2f;

    private float maxSpeed;

    public VisualEffect DustVFX;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();

        maxSpeed = GetComponent<S_PlayerController>().currentMaxSpeed();
    }

    // Update is called once per frame
    void Update()
    {

        m_Animator.SetInteger("Speed",Convert.ToInt16(m_Rigidbody.velocity.sqrMagnitude > minSpeedToWalk));
    }

    public void disablePickUp()
    {
        m_Animator.SetBool("PickUP", false);
    }

    public void walkDust()
    {
        Vector3 footPos = Vector3.zero;
        
        DustVFX.SetVector3("Foot_Location", footPos);
        DustVFX.Play();

        S_PlayerSound.instance.StepSound();
    }

    public void setPushing(bool b)
    {
        m_Animator.SetBool("IsPushing", b);
    }

    public void setPickUp(bool b)
    {
        m_Animator.SetBool("PickUP", b);
    }

    

    public void setPushingSpeed(dir d)
    {
        float f = Mathf.Clamp(Mathf.InverseLerp(0, maxSpeed, m_Rigidbody.velocity.sqrMagnitude), 0,1f) ;
        Vector3 v = m_Rigidbody.velocity;
        switch (d)
        {
            case dir.Left:
                if (v.x < 0) { f *= -1; }
                break;
            case dir.Right:
                if (v.x > 0) { f *= -1; }
                break;
            case dir.Front:
                if (v.z > 0) { f *= -1; }
                break;
            case dir.Back:
                if (v.z < 0) { f *= -1; }
                break;
            default: break;
        }
        m_Animator.SetFloat("PushPullSpeed", f);
    }

    
}
