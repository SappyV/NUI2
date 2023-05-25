using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloackController : MonoBehaviour
{
    public const int State_Empty = 0;
    public const int State_Up = 1;
    public const int State_Down = 2;
    public const int State_Left = 3;
    public const int State_Right = 4;

    public int currentState = 0;

    public Image image;

    public Material up_material;
    public Material down_material;
    public Material left_material;
    public Material right_material;
    public Material empty_material;
    
    Material GetCurrentMaterial()
    {
        if (currentState == State_Up)
        {
            return up_material;
        }
        else if (currentState == State_Down)
        {
            return down_material;
        }
        else if (currentState == State_Left)
        {
            return left_material;
        }
        else if (currentState == State_Right)
        {
            return right_material;
        }
        return empty_material;
    } 

    void Start()
    {
        image.material = GetCurrentMaterial();
    }

    public void SetNextState()
    {        
        currentState = currentState + 1;
        if (currentState > 4)
        {
            currentState = 0;
        }

        Debug.Log("Next State:" + currentState);
        image.material = GetCurrentMaterial();
    }

    public int GetCurrentState()
    {
        
        return currentState;
    }
}
