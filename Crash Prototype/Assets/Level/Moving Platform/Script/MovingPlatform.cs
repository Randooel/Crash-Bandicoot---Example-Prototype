using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] PlatformSO platformSO;

    void Start()
    {
        //transform.DOLocalMoveZ(10f, 5f).SetLoops(-1, LoopType.Yoyo);


        transform.DOLocalMoveZ(10f, platformSO.duration).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
