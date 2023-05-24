using UnityEngine;
using System.Collections;

public class CameraUpdater : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    void Start()
    {

    }

    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, player.position.z - 10);
    }
}