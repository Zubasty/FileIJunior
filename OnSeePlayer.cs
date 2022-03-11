class Weapon
{
    private readonly int _damage;
    private int _bullets;

    public bool CanFire => _bullets > 0;

    public Weapon(int damage, int bullets)
    {
        _damage = damage;
        _bullets = bullets;
    }

    public void AttackPlayer(Player player)
    {
        if (CanFire)
        {
            Fire();
            player.TakeDamage(_damage);
        }
    }

    private void Fire()
    {
        _bullets -= 1;
    }
}

class Player
{
    private int _health;

    public Player(int health)
    {
        _health = health;
    }

    public int Health
    {
        get => _health;
        private set
        {
            _health = Math.Max(0, _health + value);
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
}

class Bot
{
    private readonly Weapon _weapon;

    public Bot(Weapon weapon)
    {
        _weapon = weapon;
    }

    public void OnSeePlayer(Player player)
    {
        _weapon.AttackPlayer(player);
    }
}