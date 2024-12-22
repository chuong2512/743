using BabySound.Scripts;
using UnityEngine;

namespace ABCBoard.Scripts.UI
{
    public abstract class BaseScreenWithModel<TModel> : BaseScreen
    {
        public abstract void BindData(TModel model);
    }
}