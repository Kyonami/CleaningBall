using System.Collections.Generic;
using FsmDefine;

public class MonsterFSM
{
    #region Components
    private FiniteState currentState;   // 현재 상태
    private Dictionary<EState, FiniteState> stateList = new Dictionary<EState, FiniteState>();  
    // 상태키에 따른 상태를 갖고 있는 Dictionary
    #endregion

    // 시작 상태 - (이벤트) -> 출력 상태 로 연결
    public void AddStateTransition(EState _stateId, EEvent _inputEvent, EState _outputStateId)
    {
        FiniteState state = null;
        
        if (stateList.ContainsKey(_stateId))    // 시작 상태가 사전에 이미 있으면 재활용
        {
            state = stateList[_stateId];
            state.AddTransition(_inputEvent, _outputStateId);
        }
        else
        {
            state = new FiniteState(_stateId);
            state.AddTransition(_inputEvent, _outputStateId);
            stateList.Add(_stateId, state);
        }
    }
    // 현재상태 강제 설정
    public void SetCurrentState(EState _stateId)
    {
        currentState = stateList[_stateId];
    }
    // 상태 전환.
    public void StateTransition(EEvent _eventId)
    {
        EState outputStateId = currentState.OutputState(_eventId);
        currentState = stateList[outputStateId];
    }

    public EState GetCurrentStateId()
    {
        return currentState.stateId;
    }
}
