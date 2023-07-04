using System.Collections;
using UnityEngine;

public class JumpFX : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float height = 5;
    private Vector3 offset = new Vector3(0, 0.43f, 0);

    public void PlayAnimations(Transform jumper, float duration, Vector3 target)
    {
        StartCoroutine(AnimationByTime(jumper, duration, target));
    }

    private IEnumerator AnimationByTime(Transform jumper, float duration, Vector3 target)
    {
        float expiredSeconds = 0;
        float progress = 0;


        Vector3 startPosition = jumper.position;


        Vector3 direction = target - jumper.position;
        direction.y = 0f;

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Euler(0f, rotation.eulerAngles.y, 0f);


        while (progress < 1)
        {
            expiredSeconds += Time.deltaTime;
            progress = expiredSeconds / duration;
            jumper.position = Vector3.Lerp(startPosition + new Vector3(0, _animationCurve
                .Evaluate(progress) * height, 0), target + offset, progress);
            yield return null;
        }

    }
}
