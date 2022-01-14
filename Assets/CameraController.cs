using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float followAhead;
    public float smoothing;

    public Vector3 coords;

    private Vector3 _targetPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        _targetPosition = player.transform.position;

        float newX;

        if(player.GetComponent<PlayerController>().Direction == Direction.Right && followAhead < 0f){
            followAhead *= -1;
        }
        else if(player.GetComponent<PlayerController>().Direction == Direction.Left && followAhead > 0f){
            followAhead *= -1;
        }

        newX = _targetPosition.x += followAhead;

        _targetPosition.x = newX;
        _targetPosition.z = this.transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _targetPosition, smoothing * Time.deltaTime);
    }
}
