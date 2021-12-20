using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //[SerializeField] private float _offset;
    //[SerializeField] private float _offsetSmoothing;
    //private Vector3 _playerPosition;
    [SerializeField] private float _speedCoeff = 1f;

    public GameObject player;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x,
                                         transform.position.y,
                                         transform.position.z), Time.deltaTime*_speedCoeff);
       
    } 
}
