using GameWork.Core.ObjectPool;
using UnityEngine;

namespace GameWork.Unity.GameObjectPool
{
    public class GameObjectContainer : PoolableObjectContainer<GameObject>
    {
        public GameObjectContainer(GameObject @object) : base(@object)
        {
        }

        public override void SetTaken()
        {
            base.SetTaken();
            Object.SetActive(true);
        }

        public override void Return()
        {
            Object.SetActive(false);
            base.Return();
        }
    }
}
