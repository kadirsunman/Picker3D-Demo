using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public GameObject[] door;
    public GameObject[] interbodies;
    public float speed = 2.0f;
    public float rightLeftSpeed = 3.0f;
    public bool stopState = false;
    public bool door1State = false;
    public bool door2State = false;
    public bool door1OpenState = false;
    public bool door2OpenState = false;
    public Text skorBoard;

    Vector3 playerPos;

    Vector3 lastPos = new Vector3(0, 0, 0);
    void Update()
    {
        playerPos = transform.position;

        if (stopState == false)
        {
            movementPlayer();
        }
        else if (stopState == true)
        {
            transform.GetComponent<Rigidbody>().AddForce(0 * Vector3.forward);
            transform.position = lastPos;
        }

        if (door1State == true)
        {
            openTheDoor(door[0], door[1]);
            Invoke("interbodyUp1", 1f);
        }
        if (door2State == true)
        {
            Debug.Log("Girdi");
            openTheDoor(door[2], door[3]);
            
            Invoke("interbodyUp2", 1f);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "finishFlag1" && door1OpenState == false)
        {
            stopState = true;
            lastPos = transform.position;
            door1State = true;
        }
        if (col.gameObject.tag == "finishFlag2" && door2OpenState == false)
        {
            stopState = true;
            lastPos = transform.position;
            door2State = true;
        }
        if (col.gameObject.tag == "finishFlag3")
        {
            stopState = true;
            lastPos = transform.position;

            //Tebrikler Kazandınız.
        }
    }
    void openTheDoor(GameObject leftDoor,GameObject rightDoor)
    {
        Quaternion targetRotation =Quaternion.Euler(new Vector3(0f, 0f, 90f));
        if (leftDoor.transform.rotation.z <90)
        {
            leftDoor.transform.rotation = Quaternion.Lerp(leftDoor.transform.rotation, targetRotation, Time.time * 0.001f);
            rightDoor.transform.rotation = Quaternion.Lerp(rightDoor.transform.rotation, targetRotation, Time.time * 0.001f);
        }
    }
    void interbodyUp1()
    {
        Vector3 targetPos = new Vector3(-0.01540923f, 0.3093042f, -1.479031f);
        interbodies[0].transform.position = Vector3.Lerp(interbodies[0].transform.position, targetPos, Time.time * 0.001f);
        if(interbodies[0].transform.position == targetPos && door1OpenState == false)
        {
            stopState = false;
            door1OpenState = true;
            CancelInvoke("interbodyUp1");
        }
    }
    void interbodyUp2()
    {
        Vector3 targetPos = new Vector3(-0.01540923f, 0.3093042f, 5.520969f);
        interbodies[1].transform.position = Vector3.Lerp(interbodies[1].transform.position, targetPos, Time.time * 0.001f);
        if (interbodies[1].transform.position == targetPos && door2OpenState == false)
        {
            stopState = false;
            door2OpenState = true;
            CancelInvoke("interbodyUp2");
        }
    }


    //Toplayıcının ileri doğru hareketi, sağa sola hareketi ve kameranın takibi yapan fonksiyon.
    void movementPlayer()
    {
        transform.GetComponent<Rigidbody>().AddForce(speed * Vector3.forward);
        //transform.Translate(Vector3.forward * -Time.deltaTime);
        playerPos = transform.position;
        if (playerPos.x < -0.45f)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                transform.Translate(Input.GetAxis("Horizontal") * Vector3.right * rightLeftSpeed * -Time.deltaTime);
            }
        }
        else if (playerPos.x > 0.23f)
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                transform.Translate(Input.GetAxis("Horizontal") * Vector3.right * rightLeftSpeed * -Time.deltaTime);
            }
        }

        {
            transform.Translate(Input.GetAxis("Horizontal") * Vector3.right * rightLeftSpeed * -Time.deltaTime);
        }

        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, playerPos.z - 1.3f);
    }
}
