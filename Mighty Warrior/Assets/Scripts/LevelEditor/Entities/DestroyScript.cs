using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyScript : MonoBehaviour
{
    public int ID;

    private LevelEditorManager editor;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "LevelEditor")
        {
            editor = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
        }
    }

    void OnMouseOver()
    {
        if (SceneManager.GetActiveScene().name == "LevelEditor" && Input.GetMouseButtonDown(1))
        {
            Destroy(this.gameObject);
        }
    }
}
