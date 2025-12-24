using UnityEngine;
using TMPro;
using System.Collections;

public class Loot : MonoBehaviour
{
    private Player player;
    [SerializeField] private CollectibleSO collectibleSO;
    [SerializeField] private SpriteRenderer sr;

    public Animator anim;
    public TMP_Text itemMessage;

    [SerializeField] private bool canBeCollected;
    [SerializeField] private float collectDelay;

    public void Initialize(CollectibleSO collectibleSO)
    {
        this.collectibleSO = collectibleSO;
        sr.sprite = collectibleSO.itemSprite;

        StartCoroutine(EnableCollection());
    }

    private IEnumerator EnableCollection()
    {
        yield return new WaitForSeconds(collectDelay);
        canBeCollected = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if (player == null || !canBeCollected)
            return;

        CollectItem();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            player = null;
    }

    private void CollectItem()
    {
        itemMessage.text = "Found " + collectibleSO.itemName;
        anim.Play("CollectLoot");
        collectibleSO.Collect(player);
        Destroy(gameObject, 1);
    }
}
