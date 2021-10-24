using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconDisplay : MonoBehaviour
{
    public Sprite sprite;
    public Vector3 offsetFromParent;

    Transform player;
    PuzzleManager puzzleManager;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();

        player = puzzleManager.playerScript.gameObject.transform;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.flipX = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
        //transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        transform.position = transform.parent.position + offsetFromParent;
    }

    public void SetVisible(bool visible)
    {
        spriteRenderer.enabled = visible;
    }
}
