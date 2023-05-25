using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance = null;

    public int level = 0;

    public GameObject Diamond1;
    public GameObject Diamond2;
    public GameObject Diamond3;
    public GameObject Diamond4;
    int active_dimonds = 0;

    public BloackController block1;
    public BloackController block2;
    public BloackController block3;
    public BloackController block4;
    public BloackController block5;
    public BloackController block6;

    public RobotController robotController;

    void Start()
    {
        Instance = this;
        active_dimonds = 4;
    }

    void Update()
    {
        if (Input.GetKeyDown( "space" ))
        {
            Debug.Log( "Block1: " + block1.GetCurrentState());
            Debug.Log( "Block2: " + block2.GetCurrentState());
            Debug.Log( "Block3: " + block3.GetCurrentState());
            Debug.Log( "Block4: " + block4.GetCurrentState());
            Debug.Log( "Block5: " + block5.GetCurrentState());
            Debug.Log( "Block6: " + block6.GetCurrentState());
        }
    }
    public void RunCommands()
    {
        Debug.Log( "Block1: " + block1.GetCurrentState() );
        Debug.Log( "Block2: " + block2.GetCurrentState() );
        Debug.Log( "Block3: " + block3.GetCurrentState() );
        Debug.Log( "Block4: " + block4.GetCurrentState() );
        Debug.Log( "Block5: " + block5.GetCurrentState() );
        Debug.Log( "Block6: " + block6.GetCurrentState() );


        List<int> new_blocks = new List<int>();

        new_blocks.Clear();
        new_blocks.Add( block1.GetCurrentState() );
        new_blocks.Add( block2.GetCurrentState() );
        new_blocks.Add( block3.GetCurrentState() );
        new_blocks.Add( block4.GetCurrentState() );
        new_blocks.Add( block5.GetCurrentState() );
        new_blocks.Add( block6.GetCurrentState() );

        active_dimonds = 4;
        Diamond1.SetActive( true );
        Diamond2.SetActive( true );
        Diamond3.SetActive( true );
        Diamond4.SetActive( true );

        robotController.ExecuteBlocks( new_blocks );
    }

    public void DiamondCollected()
    {
        active_dimonds--;
    }

    public void RobotCommandFinished()
    {
        Debug.Log( "Robot Done" );

        if (active_dimonds <= 0)
        {
            Debug.Log( "Mission completed!" );
        } else
        {
            Debug.Log( "Failed" );
        }
    }
}

