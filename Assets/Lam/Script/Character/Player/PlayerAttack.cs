using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AttackArcher
{
    [SerializeField] float _mana;
    [SerializeField] PlayerUI _playerUI;

    protected override void Start() {
        base.Start();
        _playerUI.StartDataMana(_mana);
    }
}
