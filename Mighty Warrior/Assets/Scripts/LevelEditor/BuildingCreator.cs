using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BuildingCreator : Singleton<BuildingCreator>
{
    public GameObject buildingOverlay;
    bool overlayActive = true;

    [SerializeField] Tilemap previewMap, defaultMap;
    PlayerInput playerInput;

    TileBase tileBase;
    BuildingObjectBase selectedObj;

    Camera _camera;

    Vector2 mousePos;
    Vector3Int currentGridPosition;
    Vector3Int lastGridPosition;

    bool holdActive;
    Vector3Int holdStartPosition;
    BoundsInt bounds; //corners of a box aka rectangle
    
    protected override void Awake()
    { 
        base.Awake();
        playerInput = new PlayerInput();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        playerInput.Enable();

        playerInput.Gameplay.MousePosition.performed += OnMouseMove;

        playerInput.Gameplay.MouseLeftClick.performed += OnLeftClick;
        playerInput.Gameplay.MouseLeftClick.started += OnLeftClick;
        playerInput.Gameplay.MouseLeftClick.canceled += OnLeftClick;

        playerInput.Gameplay.MouseRightClick.performed += OnRightClick;
        playerInput.Gameplay.Tab.performed += OnTabPressed;
    }

    private void OnDisable()
    {
        playerInput.Disable();

        playerInput.Gameplay.MousePosition.performed -= OnMouseMove;

        playerInput.Gameplay.MouseLeftClick.performed -= OnLeftClick;
        playerInput.Gameplay.MouseLeftClick.started -= OnLeftClick;
        playerInput.Gameplay.MouseLeftClick.canceled -= OnLeftClick;

        playerInput.Gameplay.MouseRightClick.performed -= OnRightClick;
        playerInput.Gameplay.Tab.performed -= OnTabPressed;
    }

    // Setter
    private BuildingObjectBase SelectedObj
    {
        set
        {
            selectedObj = value;

            tileBase = selectedObj != null ? selectedObj.TileBase : null;

            UpdatePreview();
        }
    }

    private Tilemap tilemap
    {
        get
        {
            if (selectedObj != null && selectedObj.Category != null && selectedObj.Category.Tilemap != null){
                return selectedObj.Category.Tilemap;
            }

            return defaultMap;
        }
    }

    private void Update()
    {
        if (selectedObj != null && (SceneManager.GetActiveScene().name == "LevelEditor"))
        {
            Vector3 pos = _camera.ScreenToWorldPoint(mousePos);
            Vector3Int gridPos = previewMap.WorldToCell(pos);

            if (gridPos != currentGridPosition)
            {
                lastGridPosition = currentGridPosition;
                currentGridPosition = gridPos;

                UpdatePreview();

                if (holdActive)
                {
                    HandleDrawing();
                }
            }
        }
    }

    private void OnMouseMove(InputAction.CallbackContext ctx)
    {
        mousePos = ctx.ReadValue<Vector2>();
    }

    private void OnLeftClick(InputAction.CallbackContext ctx)
    {
        if(selectedObj != null && !EventSystem.current.IsPointerOverGameObject())
        {
            if (ctx.phase == InputActionPhase.Started)
            {
                    holdActive = true;

                if(ctx.interaction is TapInteraction)
                {
                    holdStartPosition = currentGridPosition;
                }
                
                HandleDrawing();
            }
            else
            {
                //performed or canceled
                if (ctx.interaction is SlowTapInteraction || ctx.interaction is TapInteraction && ctx.phase == InputActionPhase.Performed)
                {
                    holdActive = false;
                    //draw on release
                    HandleDrawRelease();
                }
            }
            
        }
    }

    private void OnRightClick(InputAction.CallbackContext ctx)
    {
        SelectedObj = null;
    }

    private void OnTabPressed(InputAction.CallbackContext ctx)
    {
        if (overlayActive)
        {
            buildingOverlay.SetActive(false);
            overlayActive = false;
        }
        else
        {
            buildingOverlay.SetActive(true);
            overlayActive = true;
        }
    }

    public void ObjectSelected(BuildingObjectBase obj)
    {
        SelectedObj = obj;
    }

    private void UpdatePreview()
    {
        previewMap.SetTile(lastGridPosition, null);
        previewMap.SetTile(currentGridPosition, tileBase);
    }

    private void HandleDrawing()
    {
        if(selectedObj != null)
        {
            switch (selectedObj.PlaceType)
            {
                case PlaceType.Single: 
                default:
                    DrawItem(tilemap, currentGridPosition, tileBase);
                    break;
                case PlaceType.Line:
                    LineRenderer();
                    break;
                case PlaceType.Rectangle:
                    RectangleRenderer();
                    break;
            }
        }


    }

    private void HandleDrawRelease()
    {
        if (selectedObj != null)
        {
            switch (selectedObj.PlaceType)
            {
                //Line and Rectangle are the same
                case PlaceType.Line:
                case PlaceType.Rectangle:
                    DrawBounds(tilemap);
                    previewMap.ClearAllTiles();
                    break;
            }
        }
    }

    private void RectangleRenderer()
    {
        //Render preview on UI map, draw real one on release

        previewMap.ClearAllTiles();
        // start position can be top left, but also bottom right (4 cases)

        bounds.xMin = currentGridPosition.x < holdStartPosition.x ? currentGridPosition.x : holdStartPosition.x;
        bounds.xMax = currentGridPosition.x > holdStartPosition.x ? currentGridPosition.x : holdStartPosition.x;
        bounds.yMin = currentGridPosition.y < holdStartPosition.y ? currentGridPosition.y : holdStartPosition.y;
        bounds.yMax = currentGridPosition.y > holdStartPosition.y ? currentGridPosition.y : holdStartPosition.y;

        DrawBounds(previewMap);
    }

    private void LineRenderer()
    {
        previewMap.ClearAllTiles();

        float diffX = Mathf.Abs(currentGridPosition.x - holdStartPosition.x);
        float diffY = Mathf.Abs(currentGridPosition.y - holdStartPosition.y);

        bool lineIsHorizontal = diffX >= diffY;

        if (lineIsHorizontal)
        {
            bounds.xMin = currentGridPosition.x < holdStartPosition.x ? currentGridPosition.x : holdStartPosition.x;
            bounds.xMax = currentGridPosition.x > holdStartPosition.x ? currentGridPosition.x : holdStartPosition.x;
            bounds.yMin = currentGridPosition.y;
            bounds.yMax = currentGridPosition.y;

        }
        else
        {
            bounds.xMin = currentGridPosition.x;
            bounds.xMax = currentGridPosition.x;
            bounds.yMin = currentGridPosition.y < holdStartPosition.y ? currentGridPosition.y : holdStartPosition.y;
            bounds.yMax = currentGridPosition.y > holdStartPosition.y ? currentGridPosition.y : holdStartPosition.y;

        }

        DrawBounds(previewMap);
    }

    private void DrawBounds(Tilemap map)
    {
        //draw bounds on given map
        for (int x = bounds.xMin; x <= bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y <= bounds.yMax; y++)
            {
                DrawItem(map, new Vector3Int(x, y, 0), tileBase);
            }
        }
    }

    private void DrawItem(Tilemap map, Vector3Int position, TileBase tileBase)
    {
        if (selectedObj.GetType() == typeof(BuildingTool))
            // it is a tool
        {
            BuildingTool tool = (BuildingTool)selectedObj;

            tool.Use(position);
        }
        else
            // not a tool
        {
            tilemap.SetTile(position, tileBase);
        }


    }
}
