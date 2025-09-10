using UnityEngine;
/// <summary>
/// 被击中特效
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class HitEffect : MonoBehaviour
{
    [SerializeField]
    private float duration = 0.25f;

    private int hitEffectAmount = Shader.PropertyToID("_HitEffectAmount");

    private SpriteRenderer[] spriteRenderers;

    private Material[] materials;

    private float lerpAmount;
    void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        materials = new Material[spriteRenderers.Length];
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            materials[i] = spriteRenderers[i].material;
        }
    }

    public void AttackEffect()
    {
        lerpAmount = 0;
        LeanTween.cancel(gameObject);
        LeanTween.value(gameObject, lerpAmount, 1f, duration)
            .setEase(LeanTweenType.easeOutExpo)
            .setOnUpdate(OnLerpUpdate)
            .setOnComplete(OnLerpCompleted);
    }

    private void OnLerpUpdate(float value)
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetFloat(hitEffectAmount, value);
        }
    }

    private void OnLerpCompleted()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetFloat(hitEffectAmount, 0);
        }
    }
}
