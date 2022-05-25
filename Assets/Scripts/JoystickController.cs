using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public float _speed;
    public FloatingJoystick _floatingJoystick;
    public Rigidbody _rigidbody;

    public void FixedUpdate()
    {
        if (GameController.instance.isContinue == true)
        {
            Vector3 direction = Vector3.forward * _floatingJoystick.Vertical + Vector3.right * _floatingJoystick.Horizontal;
            // transform.Translate(direction * Time.deltaTime * _speed);
            _rigidbody.velocity = new Vector3(_floatingJoystick.Horizontal * _speed, _rigidbody.velocity.y, _floatingJoystick.Vertical * _speed);

            if (_floatingJoystick.Horizontal != 0 || _floatingJoystick.Vertical != 0)
            {
                transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            }
            //transform.Rotate(0, _floatingJoystick.Horizontal * 1f, 0);
        }
        else
        {

        }

    }
}
