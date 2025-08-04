
using UnityEngine;

namespace UselessFrame.UIElements
{
    public interface IUIGroup
    {
        IUIManager Manager { get; }

        RectTransform Root { get; }

        int Count { get; }

        /// <summary>
        /// 是否处于打开状态
        /// </summary>
        bool IsOpen { get; }

        /// <summary>
        /// 组内UI整体不透明度
        /// </summary>
        float Alpha { get; set; }

        /// <summary>
        /// UI组层级, 层级大的在层级小的上层显示
        /// </summary>
        int Layer { get; set; }

        /// <summary>
        /// 打开UI组
        /// </summary>
        void Open();

        /// <summary>
        /// 关闭UI组
        /// </summary>
        void Close();

    }
}
