using UnityEngine;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour
{
    public Image iconImage;
    public GameObject highlight;
    public SpellSO AssignedSpell {  get; private set; }

    [SerializeField] private Color normalColour;
    [SerializeField] private Color highlightColour = Color.white;
    private Vector3 normalScale = Vector3.one;
    private Vector3 highlightScale = Vector3.one * 1.2f;

    public void SetSpell(SpellSO spellSO)
    {
        AssignedSpell = spellSO;

        if (spellSO != null)
        {
            iconImage.sprite = spellSO.icon;
            iconImage.gameObject.SetActive(true);
        }
        else
        {
            AssignedSpell = null;
            iconImage.sprite = null;
            iconImage.gameObject.SetActive(false);
        }

        SetHighlight(false);
    }

    public void SetHighlight(bool active)
    {
        highlight.SetActive(active);

        iconImage.color = active ? highlightColour : normalColour;
        iconImage.rectTransform.localScale = active ? highlightScale : normalScale;
    }
}
