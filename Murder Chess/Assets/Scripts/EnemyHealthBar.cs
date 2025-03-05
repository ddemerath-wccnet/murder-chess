using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public BasePiece piece;

    public float pieceMaxHealth;
    public float pieceCurrentHealth;
    public GameObject pieceMaxHealthBar;
    public GameObject pieceCurrentHealthBar;
    public float SpriteSizeMulti = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {                // IMPORTAINT!!!!!!!!!!
					//find a better way to do this this is stupid, it happens EVERY FRAME
        pieceMaxHealth = piece.MaxPieceHealth;
        pieceCurrentHealth = piece.PieceHealth;
        pieceMaxHealthBar.transform.localScale = new Vector3(pieceMaxHealth * SpriteSizeMulti, 1, 1);
        pieceCurrentHealthBar.transform.localScale = new Vector3(pieceCurrentHealth / pieceMaxHealth, 1, 1);
    }
}
