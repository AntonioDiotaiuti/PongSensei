using UnityEngine;

public interface IPlayerShooter
{
    void Reload();
    bool HasAmmo {  get; }
}
