using UnityEngine;

public class TextureScroller : MonoBehaviour
{
    [SerializeField] private Renderer meshRenderer;
    private Material material;
    [SerializeField] private Vector2 offsetPerSecond;
    [SerializeField] private Vector2 currentOffset;

    void Start()
    {
        material = meshRenderer.material;
    }

    void Update()
    {
        if (material != null)
        {
            currentOffset += offsetPerSecond * Time.deltaTime;
            if(currentOffset.y > 15f | currentOffset.y < -15f)
            {
                currentOffset.y = 0f;
            }
            if(currentOffset.x > 15f | currentOffset.x < -15f)
            {
                currentOffset.x = 0f;
            }
            material.SetTextureOffset("_BaseMap", currentOffset);
        }
    }
}
