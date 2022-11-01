using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class ToolController : Singleton<ToolController>
{
    List<Tilemap> tilemaps = new List<Tilemap>();

    private void Start()
    {
        List<Tilemap> maps = FindObjectsOfType<Tilemap>().ToList();

        maps.ForEach(map =>
        {
            if (map.name != "BuildPreview")
            {
                tilemaps.Add(map);
            }
        });

        //tilemaps.Sort((a, b) => {
        //    TilemapRenderer aRenderer = a.GetComponent<TilemapRenderer>();
        //    TilemapRenderer bRenderer = b.GetComponent<TilemapRenderer>();

        //    return bRenderer.sortingOrder.CompareTo(aRenderer.sortingOrder); //Higher values on top, lowers in the end
        //});
    }

    public void Eraser(Vector3Int position)
    {
        Debug.Log("Use Eraser");

        tilemaps.ForEach(map =>
        {
            map.SetTile(position, null);
        });
    }

    public void Player(Vector3Int position)
    {
        Debug.Log("Use Player");

        //Place player
    }

    public void EndBox(Vector3Int position)
    {
        Debug.Log("Use EndBox");

        //Place EndBox
    }
    public void Box(Vector3Int position)
    {
        Debug.Log("Use Box");

        //Place Box
    }

    public void Enemy(Vector3Int position)
    {
        Debug.Log("Use Enemy");

        //Place Box
    }

}
