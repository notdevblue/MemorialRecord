using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffectManager : MonoBehaviour
{
    [SerializeField] ParticleSystem _touchParticle;

    Queue<ParticleSystem> _effectPool = new Queue<ParticleSystem>();

    public ParticleSystem GetEffect(Vector2 pos)
    {
        ParticleSystem result = null;

        if(_effectPool.Count > 0)
        {
            if(_effectPool.Peek().gameObject.activeSelf)
            {
                result = MakeEffect();
            }
            else
            {
                result = _effectPool.Dequeue();

                _effectPool.Enqueue(result);
            }
        }
        else
        {
            result = MakeEffect();
        }

        result.transform.position = pos;
        result.gameObject.SetActive(true);

        return result;
    }

    public ParticleSystem MakeEffect()
    {
        ParticleSystem result = null;

        result = Instantiate<ParticleSystem>(_touchParticle, transform);
        result.gameObject.SetActive(false);
        _effectPool.Enqueue(result);

        return result;
    }
}
