using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class SpineHitEffect : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    [SerializeField]
    private float duration = 0.25f;
    private int fillPhase = Shader.PropertyToID("_FillPhase");

    private float lerpFillPhase;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //Test
            AttackEffect();
        }
    }

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    /// <summary>
    /// 被击中特效
    /// </summary>
    public void AttackEffect()
    {
        //Test
        lerpFillPhase = 0;
        LeanTween.cancel(gameObject);
        LeanTween.value(gameObject, lerpFillPhase, 1f, duration)
            .setEase(LeanTweenType.easeOutExpo)
            .setOnUpdate(OnLerpUpdate)
            .setOnComplete(OnLerpCompleted);
    }

    private void OnLerpUpdate(float value)
    {
        meshRenderer.material.SetFloat(fillPhase, value);
    }

    private void OnLerpCompleted()
    {
        meshRenderer.material.SetFloat(fillPhase, 0);
    }
}
