using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.takeDamage(playerStats.Damage);
        }
    }
}
