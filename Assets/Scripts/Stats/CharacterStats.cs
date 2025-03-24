using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat damage;
    public Stat maxHp;
    [SerializeField] public int currentHealth;
    protected virtual void Start()
    {
        currentHealth = maxHp.GetValue();
    }
    public virtual void DoDamage(CharacterStats _tagetStats)
    {
        int totalDamage = damage.GetValue();
        _tagetStats.TakeDamage(totalDamage);
    }

    public virtual void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        Debug.Log(_damage);

        if (currentHealth <= 0)
            Die();
    }
    protected virtual void Die()
    {}
}
