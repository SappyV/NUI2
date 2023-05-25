using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{


    CharacterController controller;

    public GameObject left_arm;

    public float movementSpeed = 1.0f;
    public float leaningAngle = 30.0f;

    private float lastAngle = 0;
    private int init = 100;

    [SerializeField]
    private Vector3 velocity = Vector3.zero;

    public Transform startingPosition;
    public Transform targetPosition;

    public Vector3 targetPos;

    public List<int> blocks;
    int currentBlock;
    private bool canMove = true;

    Vector3 initialPos;

    void Start()
    {
        lastAngle = transform.rotation.eulerAngles.y;
        controller = GetComponent<CharacterController>();

        initialPos = startingPosition.localPosition;
        //currentBlock = 0;
        //targetPosition = startingPosition;
        //targetPosition.localPosition += new Vector3(2.0f, 0.0f, 0.0f);

        RobotReset();
    }

    public void RobotReset()
    {
        currentBlock = -1;
        transform.localPosition = initialPos;
        targetPosition = startingPosition;
        startingPosition.localPosition = initialPos;
        targetPosition.localPosition = initialPos;
        canMove = false;
        blocks.Clear();
    }


    public void ExecuteBlocks(List<int> new_blocks)
    {
        RobotReset();
        blocks = new_blocks;
        canMove = true;
    }

    private void Update()
    {
        if (canMove == false)
        {
            return;
        }

        Vector3 direction = targetPosition.localPosition - transform.localPosition;

        if (direction.sqrMagnitude < 0.1)
        {
            // reached the next target
            Debug.Log("Go to next block");
            currentBlock++;

            if (currentBlock < blocks.Count)
            {
                int block = blocks[currentBlock];
                if (block == BloackController.State_Up) // up
                {
                    targetPosition.localPosition += new Vector3(0, 0, 2.0f);
                    Debug.Log("UP");
                }
                else if (block == BloackController.State_Right) // right
                {
                    targetPosition.localPosition += new Vector3( 2.0f, 0, 0 );
                    Debug.Log( "RIGHT" );
                }
                else if (block == BloackController.State_Down) // Down
                {
                    targetPosition.localPosition += new Vector3( 0, 0, -2.0f );
                    Debug.Log( "down" );
                }
                else if (block == BloackController.State_Left) // left
                {
                    targetPosition.localPosition += new Vector3( -2.0f, 0, 0.0f );
                    Debug.Log( "Left" );
                }
                else
                {
                    currentBlock++;
                    return;
                }
            }
            else
            {
                GameManager.Instance.RobotCommandFinished();
                canMove = false;
                return;
            }

        }


        float gravity = -9.8f * Time.deltaTime;

        if (controller.isGrounded)
        {
            gravity = 0;
        }

        if (init-- > 0)
        {
            gravity = -9.8f * Time.deltaTime;
        }

        Vector3 movement = Vector3.zero;

        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(leaningAngle, targetAngle, 0.0f);

            direction = direction.normalized;

            movement = direction * movementSpeed * Time.deltaTime;
            movement.y = gravity;

            left_arm.gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);

            lastAngle = targetAngle;

        }
        else
        {
            transform.rotation = Quaternion.Euler(0.0f, lastAngle, 0.0f);

            left_arm.gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 45.0f);

            movement.y = gravity;
        }

        controller.Move(movement);
        velocity = controller.velocity;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Diamond")
        {
            other.gameObject.SetActive(false);

            GameManager.Instance.DiamondCollected();
        }
    }



}
