public void Enable()
{
    _enable = true;
    _effects.StartEnableAnimation();
}

public void Disable()
{
    _enable = enable;
    _pool.Free(this);
}