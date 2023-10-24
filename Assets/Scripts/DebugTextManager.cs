using TMPro;
using UnityEngine;

public class DebugTextManager : MonoBehaviour
{
    [SerializeField] private DebugScriptableObject _debugSO;
    [SerializeField] private TMP_Text _RootstateTextfield;
    [SerializeField] private TMP_Text _SubstateTextfield;

    // Update is called once per frame
    void Update()
    {
        _RootstateTextfield.text = _debugSO.rootState;
        _SubstateTextfield.text = _debugSO.substate;
    }
}
