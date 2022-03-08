class Weapon
{
    private int _damage;
    private int _bullets;

    public int Damage => _damage;

    public int Bullets => _bullets;

    public bool TryFire(Player player = null)
    {
        if (_bullets > 0)
        {
            if (player != null)
                player.TakeDamage(Damage);
            _bullets -= 1;
            return true;
        }
        return false;
    }
}

class Player
{
    private int _health;

    public int Health
    {
        get => _health;
        private set
        {
            _health = Math.Max(0, _health - value);
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
}

class Bot
{
    private Weapon _weapon;

    public void OnSeePlayer(Player player)
    {
        _weapon.TryFire(player);
    }
}