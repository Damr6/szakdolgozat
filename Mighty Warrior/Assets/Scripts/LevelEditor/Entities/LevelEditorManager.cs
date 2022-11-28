using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEditorManager : MonoBehaviour
{

    public ItemController[] ItemButtons;
    public GameObject[] ItemPrefabs;
    public static GameObject[] PublicItemPrefabs;
    public GameObject[] ItemImage;
    public int CurrentButtonPressed;

    private void Awake()
    {
        PublicItemPrefabs = ItemPrefabs; //static, FindEntities hasznalja
    }

private void Update()
    {
        if (SceneManager.GetActiveScene().name == "LevelEditor")
        {
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);



            if (Input.GetMouseButtonDown(0) && ItemButtons[CurrentButtonPressed].Clicked)
            {
                ItemButtons[CurrentButtonPressed].Clicked = false;
                Instantiate(ItemPrefabs[CurrentButtonPressed], new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity);
                Destroy(GameObject.FindGameObjectWithTag("ItemImage"));
            }
        }
    }

}
