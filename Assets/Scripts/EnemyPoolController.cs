using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<EnemyController> _pool = new List<EnemyController>();
    void Start()
    {
        
    }

    public void Add(EnemyController enemyController)
    {
        _pool.Add(enemyController);
    }

    public EnemyController Get()
    {
        if (_pool.Count != 0)
        {
            EnemyController enemyController = _pool[0];
            _pool.Remove(enemyController);
            return enemyController;
        }
        else
        {
            return null;
        }
    }

    public bool IsEmpty() {
        if (_pool.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
