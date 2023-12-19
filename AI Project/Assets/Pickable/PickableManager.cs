using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickableManager : MonoBehaviour
{
    [SerializeField]private Player _player;

    public ScoreManager _scoreManager;

    private List<Pickable> _pickableList = new List<Pickable>();
    void Start()
    {
        InitPickableList();
    }

    private void InitPickableList()
    {
        Pickable[] pickableObjects = GameObject.FindObjectsOfType<Pickable>();
        for(int i = 0; i < pickableObjects.Length; i++)
        {
            _pickableList.Add(pickableObjects[i]);
            pickableObjects[i].OnPicked += OnPickablePick;
        }
        _scoreManager.SetMaxScore(_pickableList.Count);
    }

    private void OnPickablePick(Pickable pickable)
    {
        _pickableList.Remove(pickable);
        if(_scoreManager != null)
        {
            _scoreManager.AddScore(1);
        }
        if (pickable.PickableType == PickableType.PowerUp)
        {
            _player.PickPowerUp();
        }
        if (_pickableList.Count <= 0)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
