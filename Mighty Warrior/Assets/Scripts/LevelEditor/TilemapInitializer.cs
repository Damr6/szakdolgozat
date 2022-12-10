using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapInitializer : Singleton<TilemapInitializer> //Makes sure the class only exists once
{
    [SerializeField] List<BuildingCategory> categoriesToCreateTilemapsFor;
    [SerializeField] Transform grid;

    private void Start()
    {
        CreateMaps();
    }

    private void CreateMaps()
    {
        foreach (BuildingCategory category in categoriesToCreateTilemapsFor)
        {
            GameObject obj = new GameObject("Tilemap_" + category.name);
            Tilemap map = obj.AddComponent<Tilemap>();
            TilemapRenderer tr = obj.AddComponent<TilemapRenderer>();

            if (obj.name == "Tilemap_Wall")
            {
                TilemapCollider2D col = obj.AddComponent<TilemapCollider2D>();
            }

            obj.transform.SetParent(grid,false); // worldPositionStays false

            //Settings
            tr.sortingOrder = category.SortingOrder;

            category.Tilemap = map;
        }
    }
}
