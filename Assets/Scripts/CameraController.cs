using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float offset;
    public float smoothing;

    private Vector3 _targetPosition;
    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        _targetPosition = transform.position;

        //flipping offset direction before applying to camera
        if ((_playerController.Direction == Direction.Right && offset < 0f) ||
             (_playerController.Direction == Direction.Left && offset > 0f))
        {
            offset *= -1;
        }

        _targetPosition.x = _playerController.transform.position.x + offset;

        transform.position = Vector3.Lerp(transform.position, _targetPosition, smoothing * Time.deltaTime);
    }
}
