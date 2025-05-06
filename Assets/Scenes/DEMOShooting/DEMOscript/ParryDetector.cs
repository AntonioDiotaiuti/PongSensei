using UnityEngine;


using System.Collections.Generic;

public class ParryDetector : MonoBehaviour
{
    private List<GameObject> projectilesInRange = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile") && !projectilesInRange.Contains(other.gameObject))
        {
            projectilesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            projectilesInRange.Remove(other.gameObject);
        }
    }

    public bool HasProjectileInRange()
    {
        // Rimuove eventuali oggetti distrutti
        projectilesInRange.RemoveAll(item => item == null);
        return projectilesInRange.Count > 0;
    }
}

