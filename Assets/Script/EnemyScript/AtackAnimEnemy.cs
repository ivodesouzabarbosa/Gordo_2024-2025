using UnityEngine;

public class AtackAnimEnemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public bool _checkAtack;

    // Update is called once per frame
    public void AtackHitTrue()
    {
        _checkAtack = true;
    }
    public void AtackHitFalse()
    {
        _checkAtack = false;
    }
}
