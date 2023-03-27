using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 ---> need bottom door
    // 2 ---> need top door
    // 3 ---> need left door
    // 4 ---> need right door
    
    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    public float waitTime = 4f;

    void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }


    void Spawn()
    {
        if (spawned == false)
        {
            var rand = Random.Range(0, templates.bottomRooms.Length);
            if (openingDirection == 1)
            {
                //need to spawn room with BOTTOM door
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);  //if want no rotation switch to 'Quaternion.identity'
            }
            else if (openingDirection == 2)
            {
                //need to spawn a room with TOP door
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);  //if want no rotation switch to 'Quaternion.identity'
            }
            else if (openingDirection == 3)
            {
                //need to spawn a room with LEFT door
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);  //if want no rotation switch to 'Quaternion.identity'
            }
            else if (openingDirection == 4)
            {
                //need to spawn a room with a RIGHT door
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);  //if want no rotation switch to 'Quaternion.identity'
            }
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if(other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                //spawn walls blocking off any opening
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;

        }
    }
}
