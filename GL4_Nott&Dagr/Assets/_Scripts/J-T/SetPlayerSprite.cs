using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SetPlayerSprite : MonoBehaviour
{
    private PlayerController player;

    public SpriteRenderer playerSprite, auraSprite;
    public Sprite nightAura, dayAura, nightSprite, daySprite;

    private void Start()
    {
        playerSprite.gameObject.SetActive(true);
        player = GetComponent<PlayerController>();

        if (player.setPlayer.ToString() == "Night")
        {
            playerSprite.sprite = nightSprite;
            auraSprite.sprite = nightAura;
        }
        if (player.setPlayer.ToString() == "Day")
        {
            playerSprite.sprite = daySprite;
            auraSprite.sprite = dayAura;
        }
    }
}
