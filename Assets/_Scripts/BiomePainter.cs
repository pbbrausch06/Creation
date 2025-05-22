using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static BiomeObject;

public class BiomePainter : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int gridWidth = 100;
    [SerializeField] private int gridHeight = 100;
    [SerializeField] private float cellSize = 1f;

    [Header("Brush Settings")]
    public float brushRadius = 2f;

    [Header("Overlay & Input")]
    public Material overlayMaterial;

    private BiomeObject[,] biomeGrid;
    private Texture2D baseOverlayTexture;
    private Texture2D previewOverlayTexture;
    private List<Vector2Int> previewCells = new();
    private bool showOverlay = true;
    private BiomeObject selectedBiome;

    private void Awake()
    {
        PlayerManager.Instance.playerInput.Actions.LeftClick.started += StartDragging;
        PlayerManager.Instance.playerInput.Actions.LeftClick.performed += ContinueDragging;
        PlayerManager.Instance.playerInput.Actions.LeftClick.canceled += StopDragging;

        biomeGrid = new BiomeObject[gridWidth, gridHeight];

        baseOverlayTexture = new(gridWidth, gridHeight);
        previewOverlayTexture = new(gridWidth, gridHeight);

        overlayMaterial.mainTexture = previewOverlayTexture;

        ClearTexture(baseOverlayTexture);
         
        CopyTexture(baseOverlayTexture, previewOverlayTexture);
    }

    private void StartDragging(InputAction.CallbackContext _)
    {
        if (!selectedBiome) { return; }

        previewCells.Clear();
    }

    private void ContinueDragging(InputAction.CallbackContext _)
    {
        if (!selectedBiome) { return; }

        HandleBrushDrag();
    }

    private void StopDragging(InputAction.CallbackContext _)
    {
        if (!selectedBiome) { return; }

        if (!PlayerManager.Instance.playerStats.CanAfford(GetAreaCost()))
        {
            CopyTexture(baseOverlayTexture, previewOverlayTexture);
            return;
        }

        PlayerManager.Instance.playerStats.Buy(GetAreaCost());

        foreach (var c in previewCells)
        {
            biomeGrid[c.x, c.y] = selectedBiome;
        }

        DrawBaseOverlay(baseOverlayTexture);
        CopyTexture(baseOverlayTexture, previewOverlayTexture);
    }

    private void HandleBrushDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(PlayerManager.Instance.playerInput.Pointer);
        if (!Physics.Raycast(ray, out RaycastHit hit)) return;

        Vector2Int cell = WorldToCell(hit.point);
        AddCellsInBrush(cell);
        RenderPreview();
        UpdateCostUI();
    }

    private Vector2Int WorldToCell(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x / cellSize);
        int y = Mathf.FloorToInt(worldPos.z / cellSize);
        return new Vector2Int(
            Mathf.Clamp(x, 0, gridWidth - 1),
            Mathf.Clamp(y, 0, gridHeight - 1)
        );
    }

    private void AddCellsInBrush(Vector2Int center)
    {
        int radiusInCells = Mathf.RoundToInt(brushRadius / cellSize);
        for (int dx = -radiusInCells; dx <= radiusInCells; dx++)
            for (int dy = -radiusInCells; dy <= radiusInCells; dy++)
            {
                Vector2Int c = new(center.x + dx, center.y + dy);

                if (c.x < 0 || c.x >= gridWidth || c.y < 0 || c.y >= gridHeight) { continue; }

                if (Vector2.Distance(c, center) * cellSize <= brushRadius)
                    if (!PlayerManager.Instance.playerInput.Shifting && previewCells.Contains(c)) { continue; }

                previewCells.Add(c);
            }
    }

    private void RenderPreview()
    {
        Color previewCol = selectedBiome.OverlayColor;
        previewCol.a = 0.5f;
        foreach (var c in previewCells)
            previewOverlayTexture.SetPixel(c.x, c.y, previewCol);

        previewOverlayTexture.Apply();
    }

    private void UpdateCostUI()
    {
        //UIManager.Instance.ShowBiomeCost(GetAreaCost());
    }

    private int GetAreaCost()
    {
        float totalArea = previewCells.Count * cellSize * cellSize;
        return Mathf.CeilToInt(totalArea * selectedBiome.ResourcePerAreaUnit);
    }

    private void ClearTexture(Texture2D tex)
    {
        var clear = new Color(0, 0, 0, 0);
        for (int x = 0; x < tex.width; x++)
            for (int y = 0; y < tex.height; y++)
                tex.SetPixel(x, y, clear);
        tex.Apply();
    }

    private void CopyTexture(Texture2D src, Texture2D dst)
    {
        dst.SetPixels(src.GetPixels());
        dst.Apply();
    }

    private void DrawBaseOverlay(Texture2D tex)
    {
        for (int x = 0; x < gridWidth; x++)
            for (int y = 0; y < gridHeight; y++)
                tex.SetPixel(x, y, biomeGrid[x,y].OverlayColor);
        tex.Apply();
    }

    public void ToggleOverlay()
    {
        showOverlay = !showOverlay;
        overlayMaterial.SetFloat("_Alpha", showOverlay ? 1f : 0f);
    }

    public void UpdateSelectedBiome(BiomeObject biomeObject)
    {
        selectedBiome = biomeObject;
    }
}
