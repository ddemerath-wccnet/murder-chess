using System.Collections;
using UnityEngine;

public class KnightLevel2 : Knight
{
    [SerializeField] private float stunRadius = 1.5f;
    [SerializeField] private float stunDuration = 1f;

    protected override IEnumerator JumpAnimation(Vector3 targetPosition)
    {
        isJumping = true;
        float jumpDuration = 0.6f;
        float peakHeight = 1.0f;
        Vector3 startPosition = transform.position;

        float elapsedTime = 0f;
        while (elapsedTime < jumpDuration)
        {
            float t = elapsedTime / jumpDuration;
            float heightOffset = Mathf.Sin(t * Mathf.PI) * peakHeight;

            transform.position = Vector3.Lerp(startPosition, targetPosition, t) + new Vector3(0, heightOffset, 0);
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.3f, Mathf.Sin(t * Mathf.PI));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.one;

        if (landingDustPrefab != null)
        {
            ParticleSystem dust = Instantiate(landingDustPrefab, targetPosition - new Vector3(0, 0.3f, 0), Quaternion.identity);
            dust.Play();
            Destroy(dust.gameObject, dust.main.duration + dust.main.startLifetime.constantMax);
        }

        TryStunPlayer(targetPosition);

        isJumping = false;
    }

    private void TryStunPlayer(Vector3 center)
    {
        GameObject playerObj = GlobalVars.player;
        if (playerObj == null) return;

        float distance = Vector3.Distance(center, playerObj.transform.position);
        if (distance <= stunRadius && GlobalVars.multiplier_PlayerSpeed > 0f)
        {
            StartCoroutine(TemporarilySlowPlayer(stunDuration));
        }
    }

    private IEnumerator TemporarilySlowPlayer(float duration)
    {
        float originalMultiplier = GlobalVars.multiplier_PlayerSpeed;

        GlobalVars.multiplier_PlayerSpeed = 0f;
        yield return new WaitForSeconds(duration);
        GlobalVars.multiplier_PlayerSpeed = originalMultiplier;
    }
}
