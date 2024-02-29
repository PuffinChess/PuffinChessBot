using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _tilePiece;

    [SerializeField] private Transform _cam;

    void Start()
    {
        GenerateChessBoard();
    }

    void GenerateChessBoard()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePiece, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {((char)(x + 'a'))} {y + 1}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
            }
        }

        _cam.transform.position = new Vector3(_width / 2, _height / 2, -10);
    }
}
