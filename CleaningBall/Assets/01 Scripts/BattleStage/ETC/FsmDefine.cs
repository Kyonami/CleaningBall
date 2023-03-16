namespace FsmDefine
{
    public enum EState
    {
        STATE_IDLE = 0,
        STATE_PATROL,
        STATE_ATTACK,
        STATE_DEAD
    }
    public enum EEvent
    {
        EVENT_FINDTARGET = 0,
        EVENT_LOSTTARGET,
        EVENT_BEATATTACK,
        EVENT_STOPWALK,
        EVENT_PATROL,
        EVENT_DEAD,
        EVENT_REVIVE
    }
}