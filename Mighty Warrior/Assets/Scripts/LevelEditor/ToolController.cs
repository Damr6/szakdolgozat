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
        }

            );
    }

    public void Eraser(Vector3Int position)
    {
        Debug.Log("Use Eraser");

        tilemaps.ForEach(map =>
        {
            map.SetTile(position, null);
        });
    }

}
