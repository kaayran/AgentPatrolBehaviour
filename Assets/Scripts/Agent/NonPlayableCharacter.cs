using UnityEngine;

namespace Agent;

public class NonPlayableCharacter : MonoBehaviour
{
    [SerializeField] private int _ammoCount;

    private Material _material;

    private void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    public int GetCurrentAmmo()
    {
        return _ammoCount;
    }

    public float GetRangeToEnemy()
    {
        throw new System.NotImplementedException();
    }

    public void SetColor(Color color)
    {
        _material.color = color;
    }
}