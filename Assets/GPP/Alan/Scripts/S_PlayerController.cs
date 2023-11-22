using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]

public class S_PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private S_LeftStick _leftStick;

    [SerializeField] private float _moveSpeed;

    private bool isNotInMenu = true;

    private void FixedUpdate()
    {
        Debug.Log(isNotInMenu);
        _rigidbody.velocity = new Vector3(_leftStick.Horizontal * _moveSpeed * Convert.ToSingle(isNotInMenu), _rigidbody.velocity.y, _leftStick.Vertical * _moveSpeed * Convert.ToSingle(isNotInMenu));

        if (_leftStick.Horizontal != 0 || _leftStick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        }
    }

    public void setIsNotInMenu(bool b) {isNotInMenu = b;}
}
