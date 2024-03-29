using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

/// <summary>
/// Just a crappy character controller for the video
/// </summary>
public class CarTest : MonoBehaviour
{
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _cam = Camera.main;
    }

    private void Update()
    {

        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        Acceleration();
        Turn();
        Fall();
    }

    #region Movement

    [SerializeField] private float _acceleration = 80;
    [SerializeField] private float _maxVelocity = 10;
    private Vector3 _input;
    private Rigidbody _rb;

    private void Acceleration()
    {
        _rb.velocity += _input.normalized * (_acceleration * Time.deltaTime);
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _maxVelocity);
    }

    #endregion

    #region Rotation

    [SerializeField] private float _rotationSpeed = 450;
    private Plane _groundPlane = new(Vector3.up, Vector3.zero);
    private Camera _cam;

    private void Turn()
    {
        var ray = _cam.ScreenPointToRay(Input.mousePosition);

        if (_groundPlane.Raycast(ray, out var enter))
        {
            var hitPoint = ray.GetPoint(enter);

            var dir = hitPoint - transform.position;
            var rot = Quaternion.LookRotation(dir);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _rotationSpeed * Time.deltaTime);
        }
    }

    #endregion

    #region Fall

    private void Fall()
    {

    }

    #endregion
}
