using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ToolType
{
    None,
    Eraser,
    Player,
    EndBox,
    Box,
    Enemy
}


[CreateAssetMenu(fileName = "Tool", menuName = "levelBuilding/Create Tool")]
public class BuildingTool : BuildingObjectBase
{

    [SerializeField] private ToolType toolType;

    public void Use(Vector3Int position)
    {
        ToolController tc = ToolController.GetInstance();

        switch (toolType)
        {
            case ToolType.Eraser:
                tc.Eraser(position);
                break;
            case ToolType.Player:
                tc.Player(position);
                break;
            case ToolType.EndBox:
                tc.EndBox(position);
                break;
            case ToolType.Box:
                tc.Box(position);
                break;
            case ToolType.Enemy:
                tc.Enemy(position);
                break;

            default:
                Debug.Log("Tool type not set");
                break;
        }
    }

}
