using UnityEngine;
using Random = UnityEngine.Random;

namespace Navigation
{
    public class NavPointGenerator : MonoBehaviour
    {
        private MeshRenderer _renderer;
        private static float _sizeX;
        private static float _sizeY;
        private static float _sizeZ;

        private void Start()
        {
            _renderer = GetComponent<MeshRenderer>();

            var bounds = _renderer.bounds;
            _sizeX = bounds.size.x;
            _sizeY = 1f;
            _sizeZ = bounds.size.z;
            Debug.Log($"{_sizeX}, {_sizeY}, {_sizeZ}");
        }

        public static Vector3 GetRandomPoint()
        {
            var x = Random.Range(-_sizeX / 2, _sizeX / 2);
            var z = Random.Range(-_sizeZ / 2, _sizeZ / 2);

            var point = new Vector3(x, _sizeY, z);

            return point;
        }
    }
}