
using System;
using System.Threading;
using UselessFrame.UIElements;

namespace UnityXFrameLib.UIElements
{
    /// <summary>
    /// 单UI可打开组辅助器
    /// </summary>
    public class OnlyOneUIGroupHelper : UIGroupHelperInEffect
    {
        private IUI m_Opening;
        private IUI m_CurOpenUI;
        private bool m_IsOpen;

        protected override void OnInit()
        {
            base.OnInit();
            m_CurOpenUI = null;
            m_Opening = null;
            m_IsOpen = false;
        }

        protected async override void OnUIOpen(IUI ui, Action openCompleteCbk, CancellationToken cancellation)
        {
            KillClose(ui);
            m_Opening?.Close();
            if (m_CurOpenUI != ui)
                m_CurOpenUI?.Close();
            m_CurOpenUI = null;

            m_Opening = ui;
            await DoOpen(ui, cancellation);
            m_Opening = null;
            m_CurOpenUI = ui;
            openCompleteCbk();
            m_IsOpen = true;
        }

        protected override async void OnUIClose(IUI ui, Action closeCompleteCbk, CancellationToken cancellation)
        {
            KillClose(ui);
            if (m_Opening != null)
            {
                KillOpen(m_Opening);
                m_Opening = null;
            }
            if (m_CurOpenUI != null && m_CurOpenUI != ui)
            {
                KillOpen(m_CurOpenUI);
                m_CurOpenUI = null;
            }

            await DoClose(ui, cancellation);
            if (cancellation.IsCancellationRequested)
                return;

            closeCompleteCbk();
            if (m_CurOpenUI == ui)
            {
                if (m_IsOpen)
                {
                    m_IsOpen = false;
                }
                m_CurOpenUI = null;
            }
        }
    }
}
