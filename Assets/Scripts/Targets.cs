using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Targets : MonoBehaviour
{
    [SerializeField] private Target[] _allTargets;
    [SerializeField] private int _currentIndex = 0;

    private void Start()
    {
        for (int i = 0; i < _allTargets.Length; i++)
        {
            _allTargets[i].gameObject.SetActive(false);
        }
        _allTargets[_currentIndex].gameObject.SetActive(true);
        _allTargets[_currentIndex].OnWinning.AddListener(SwitchNextTarget);
    }

    private void SwitchNextTarget()
    {
        // ������������ �� ������� ��� �������� �������
        _allTargets[_currentIndex].OnWinning.RemoveListener(SwitchNextTarget);
        // ��������� ������� ������
        _allTargets[_currentIndex].gameObject.SetActive(false);

        // ��������� � ���������� �������
        _currentIndex = (_currentIndex + 1) % _allTargets.Length;

        // ���������� ��������� ������ � ������������� �� ��� �������
        _allTargets[_currentIndex].gameObject.SetActive(true);
        _allTargets[_currentIndex].OnWinning.AddListener(SwitchNextTarget);
    }
}
