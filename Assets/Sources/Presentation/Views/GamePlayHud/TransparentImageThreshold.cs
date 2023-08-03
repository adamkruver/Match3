using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TransparentImageThreshold : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float _alphaThreshold = .5f;

    private void Awake()
    {
        Image image = GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = _alphaThreshold;
    }
}