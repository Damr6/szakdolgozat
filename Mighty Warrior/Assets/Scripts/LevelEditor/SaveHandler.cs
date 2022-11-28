using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using UnityEngine.SceneManagement;

public class SaveHandler : MonoBehaviour
{
    Dictionary<string, Tilemap> tilemaps = new Dictionary<string, Tilemap>();
    Dictionary<TileBase, BuildingObjectBase> tileBaseToBuildingObject = new Dictionary<TileBase, BuildingObjectBase>();
    Dictionary<String, TileBase> guidToTileBase = new Dictionary<string, TileBase>();

    [SerializeField] BoundsInt bounds;
    [SerializeField] string filename = "TilemapData.json";

    private void Start()
    {
        initTilemaps();
        InitTileReferences();

        if (SceneManager.GetActiveScene().name.Equals("Stage1"))
        {
            Debug.Log("Map Loaded");
            OnLoad();
            FindEntities.LoadPrefabs();
        }

    }

    private void InitTileReferences()
    {
        BuildingObjectBase[] buildables = Resources.LoadAll<BuildingObjectBase>("Scriptables/Buildables");

        foreach(BuildingObjectBase buildable in buildables)
        {
            if (!tileBaseToBuildingObject.ContainsKey(buildable.TileBase))
            {
                tileBaseToBuildingObject.Add(buildable.TileBase, buildable);
                guidToTileBase.Add(buildable.name, buildable.TileBase);
            }
            else
            {
                Debug.LogError("TileBase " + buildable.TileBase.name + " is already in use by " + tileBaseToBuildingObject[buildable.TileBase].name);
            }
        }
    }

    public void initTilemaps()
    {
        //Get all tilemaps from scene
        // and write to dict

        Tilemap[] maps = FindObjectsOfType<Tilemap>();

        foreach(var map in maps)
        {
            tilemaps.Add(map.name, map);
        }
    }

    public void OnSave()
    {
        List<TilemapData> data = new List<TilemapData>();

        foreach (var mapObj in tilemaps)
        {
            TilemapData mapData = new TilemapData();
            mapData.key = mapObj.Key;

            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                for (int y = bounds.yMin; y < bounds.yMax; y++)
                {
                    Vector3Int pos = new Vector3Int(x, y, 0);
                    TileBase tile = mapObj.Value.GetTile(pos);

                    if(tile != null && tileBaseToBuildingObject.ContainsKey(tile)){
                        String guid = tileBaseToBuildingObject[tile].name;
                        TileInfo ti = new TileInfo(pos, guid);
                        // Add to list
                        mapData.tiles.Add(ti);
                    }
                }
            }

            data.Add(mapData);
        }

        // save
        FileHandler.SaveToJSON<TilemapData>(data,filename);
    }

    public void OnLoad()
    {

        List<TilemapData> data = FileHandler.ReadListFromJSON<TilemapData>(filename);
        foreach(var mapData in data)
        {
            if (!tilemaps.ContainsKey(mapData.key))
            {
                Debug.LogError("Found saved data for tilemap called '" + mapData.key + "', but tilemaps does not exist. Skip.");
                continue;
            }

            // get tilemap
            var map = tilemaps[mapData.key];

            // clear map
            map.ClearAllTiles();

            if(mapData.tiles != null && mapData.tiles.Count > 0)
            {
                foreach(TileInfo tile in mapData.tiles)
                {
                    if (guidToTileBase.ContainsKey(tile.guidForBuildable))
                    {
                        map.SetTile(tile.position, guidToTileBase[tile.guidForBuildable]);
                    }
                    else
                    {
                        Debug.LogError("Reference " + tile.guidForBuildable + " could not be found.");
                    }

                }
            }
        }
    }
}

[Serializable]
public class TilemapData
{
    public string key; //key of dict (name)
    public List<TileInfo> tiles = new List<TileInfo>();
}

[Serializable] 
public class TileInfo{

    public string guidForBuildable;
    public Vector3Int position;

    public TileInfo(Vector3Int pos, string guid)
    {
        position = pos;
        guidForBuildable = guid;
    }
}