using System.Collections.Generic;
using UnityEngine;
using FsmDefine;

public class FiniteState
{
    #region Values
    public EState stateId;  // 현재 상태 아이디
    public Dictionary<EEvent, EState> transList = new Dictionary<EEvent, EState>(); 
    // 어느 이벤트 발생시 어떤 상태로 바꾸어야 하는지를 Dictionary로 갖고 있는다.
    #endregion

    public FiniteState(EState _stateId)
    {
        stateId = _stateId;
    }

    // 이벤트 하나에 상태 하나 연결.
    public void AddTransition(EEvent _inputEvent, EState _outputState)
    {
        transList[_inputEvent] = _outputState;
    }

    // 이벤트에 따른 출력 상태를 리턴
    public EState OutputState(EEvent _inputEvent)
    {
        if (!transList.ContainsKey(_inputEvent))
        {   
            string str = string.Format("Event = {0} state = {1}", _inputEvent, stateId);
            Debug.LogError("Input Event is not vaild : " + str);
        }
        return transList[_inputEvent];
    }
}
