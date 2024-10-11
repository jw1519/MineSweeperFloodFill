using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int x, y;
    public bool isMine;
    public bool isRevealed = false;
    public int amountOfMines;

    private FloodFillGrid gridManager;
    private SpriteRenderer spriteRenderer;
    private TextMeshProUGUI textMesh;
    private void Start()
    {
        //SetColor(Color.white);
        textMesh = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        textMesh.SetText("");
    }
    public void Init(int x, int y, bool isMine, FloodFillGrid gridManager)
    {
        this.x = x;
        this.y = y;
        this.isMine = isMine;
        this.gridManager = gridManager;

        spriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.AddComponent<BoxCollider2D>();
    }
    public void OnMouseDown()
    {
        if (isMine)
        {
            SetColor(Color.red); //turn mine red
            IdentifyAmountofMines();
        }    
        else
        {
            gridManager.FloodFill(x, y);
        }
    }
    public void Reveal()
    {
        isRevealed = true;
        SetColor(Color.grey); //change color when revealed
        textMesh.SetText(amountOfMines.ToString());
    }
    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }
    public void IdentifyAmountofMines()
    {
        
    }

}
