using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMaterialColorChanger : MonoBehaviour, IMaterialColor
{
    private MeshRenderer meshRenderer;
    private MaterialPropertyBlock materialPropertyBlock;
    private Color currentColor;
    
    private static readonly int ColorPropertyName = Shader.PropertyToID("_Color");

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        materialPropertyBlock = new();

        currentColor = meshRenderer.material.color;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentColor = GetRandomColor();
            SetMaterialColor(currentColor);
        }
    }
    
    private Color GetRandomColor() => Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

    #region IMaterialColor

    public Color GetMaterialColor() => currentColor;

    public void SetMaterialColor(Color colorToSet)
    {
        materialPropertyBlock.SetColor(ColorPropertyName, colorToSet);
        meshRenderer.SetPropertyBlock(materialPropertyBlock);
    }

    #endregion
    
}
