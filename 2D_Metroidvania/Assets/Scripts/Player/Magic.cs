using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    [Header("References")]
    public Player player;
    public SpellUIManager spellUIManager;

    [Header("Spell State")]
    [SerializeField] private List<SpellSO> availableSpells = new List<SpellSO>();
    [SerializeField] private int currentIndex = 0;
    public SpellSO CurrentSpell => availableSpells.Count > 0 ? availableSpells[currentIndex] : null;
    
    private Dictionary<SpellSO, float> spellCooldowns = new Dictionary<SpellSO, float>();

    public void Start()
    {
        spellUIManager.ShowSpells(availableSpells);
        HighlightCurrentSpell();
    }

    public void LearnSpell(SpellSO spellSO)
    {
        if (!availableSpells.Contains(spellSO))
            availableSpells.Add(spellSO);

        currentIndex = Mathf.Clamp(currentIndex, 0, availableSpells.Count - 1);

        spellUIManager.ShowSpells(availableSpells);

        if (!spellCooldowns.ContainsKey(spellSO))
            spellCooldowns[spellSO] = 0;

        if (availableSpells.Count > 0)
            HighlightCurrentSpell();
    }

    public void NextSpell()
    {
        if (availableSpells.Count == 0) return;

        currentIndex = (currentIndex + 1) % availableSpells.Count;
        HighlightCurrentSpell();
    }

    public void PreviousSpell()
    {
        if (availableSpells.Count == 0) return;

        currentIndex = (currentIndex - 1 + availableSpells.Count) % availableSpells.Count;
        HighlightCurrentSpell();
    }

    private void HighlightCurrentSpell()
    {
        if (CurrentSpell != null)
            spellUIManager.HighlightSpell(CurrentSpell);
    }

    public void AnimationFinished()
    {
        player.AnimationFinished();
        CastSpell();
    }

    public bool CanCast(SpellSO spellSO)
    {
        return Time.time >= spellCooldowns[spellSO];
    }

    private void CastSpell()
    {
        if (!CanCast(CurrentSpell) || CurrentSpell == null)
            return;

        CurrentSpell.Cast(player);

        spellCooldowns[CurrentSpell] = Time.time + CurrentSpell.cooldown;        
        spellUIManager.TriggerCooldown(CurrentSpell, CurrentSpell.cooldown);
    }
}