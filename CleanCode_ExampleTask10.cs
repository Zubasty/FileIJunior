class Weapon
{
    private const int MaxBulletsCantShot = 0;
    private const int CountBulletsForShot = 1;

    private int _bullets;

    public bool CanShoot() => _bullets > MaxBulletsCantShot;

    public void Shoot() => _bullets -= CountBulletsForShot;
}