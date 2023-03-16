public class CharacterStat
{
    #region Values
    private int atkLv = 0;  // 공격 레벨
    private int hpLv = 0;   // HP 레벨
    #endregion

    #region Components
    public int AttackLevel { get => atkLv; set => atkLv = value;}
    public int HPLevel { get => hpLv; set => hpLv = value; }
    #endregion

    public CharacterStat(int _atkLv, int _hpLv)
    {
        atkLv = _atkLv;
        hpLv = _hpLv;
    }
}
