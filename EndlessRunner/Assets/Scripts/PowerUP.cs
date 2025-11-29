using UnityEngine;

public class PowerUP : TriggerReaction
{
    [SerializeField] private float bonusScore = 1f;
    [SerializeField] private float bonusTime = 10f;
    protected override void HandleTriggerEnter(Collider other)
    {
        base.HandleTriggerEnter(other);
        GameController.Instance.AddScore(bonusScore);
        GameController.Instance.IncreaseTime(bonusTime);
    }
}
