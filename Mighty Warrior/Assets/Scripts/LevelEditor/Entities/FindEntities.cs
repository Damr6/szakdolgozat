using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FindEntities : MonoBehaviour
{

    public static List<EnemyData> GetPrefabs()
    {
        List<EnemyData> myPrefabs = new List<EnemyData>();
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer.Equals(9) || obj.layer.Equals(10)) //Enemy vagy Item layer
            {
                EnemyData prefab = new EnemyData()
                {
                    type = obj.gameObject.tag,
                    position = obj.transform.position,
                    objName = obj.gameObject.name.Remove(obj.gameObject.name.Length - 7) // -(Clone)
                    
                };

                myPrefabs.Add(prefab);
            }

        }
        return myPrefabs;
    }
    public void SaveEntitiesButtonPressed()
    {
        List<EnemyData> savePrefabs = GetPrefabs();
        SaveEntities.Save(savePrefabs);
    }

    public static void DestroyPrefabs()
    {

        GameObject[] myObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in myObjects)
        {
            if (obj.layer.Equals(9) || obj.layer.Equals(10))
            {
                Destroy(obj);
            }
        }
    }

    public void DestroyEntitesButtonPressed()
    {
        DestroyPrefabs();
    }


    public static void LoadPrefabs()
    {
        DestroyPrefabs(); // Minden betoltes elott kitorlom az elozot

        GameObject[] ItemPrefabs = LevelEditorManager.PublicItemPrefabs;

        List<EnemyData> myPrefabs = SaveEntities.Load();

        if ( (ItemPrefabs != null && myPrefabs != null))
        {
            foreach (EnemyData mp in myPrefabs)
            {
                foreach (GameObject ip in ItemPrefabs)
                {
                    if (mp.type == ip.tag && mp.objName == ip.name)
                    {
                        Instantiate(ip, new Vector3(mp.position.x, mp.position.y, 0), Quaternion.identity);
                    }
                }
            }
        } 

    }

    public void LoadEntitiesButtonPressed()
    {
        LoadPrefabs();
    }

}
