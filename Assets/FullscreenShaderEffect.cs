using UnityEngine;

[ExecuteInEditMode]
public class FullscreenShaderEffect : MonoBehaviour
{
    public Material fullscreenMaterial;

    private Camera _mainCamera;
    private GameObject _quad;

    void Start()
    {
        _mainCamera = Camera.main;

        // Create a quad dynamically if it doesn't exist
        if (_quad == null)
        {
            _quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            _quad.GetComponent<MeshRenderer>().material = fullscreenMaterial;
            _quad.name = "FullscreenQuad";
            _quad.transform.parent = transform;
        }

        UpdateQuadSize();
    }

    void Update()
    {
        UpdateQuadSize();
    }

    private void UpdateQuadSize()
    {
        if (_mainCamera == null) return;

        // Get the camera's size and aspect ratio
        float cameraHeight = 2f * _mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * _mainCamera.aspect;

        // Position and scale the quad to cover the screen
        _quad.transform.position = new Vector3(_mainCamera.transform.position.x, _mainCamera.transform.position.y, _mainCamera.nearClipPlane + 0.01f);
        _quad.transform.localScale = new Vector3(cameraWidth, cameraHeight, 1f);
    }
}
