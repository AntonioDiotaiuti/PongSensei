using UnityEngine;
using System.Collections.Generic;

public class ParryManager : MonoBehaviour
{
    public MeleeAttack meleeAttack;
    public MeleeParry meleeParry;
    public ParryDetector parryDetector;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (parryDetector.HasProjectileInRange())
            {
                meleeParry.Activate();
                Debug.Log("Parry attivato");
            }
            else
            {
                meleeAttack.Activate();
                Debug.Log("Attacco melee");
            }
        }
    }
}
