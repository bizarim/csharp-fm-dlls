using fmCommon.Battle.Base;
using System.Collections.Generic;

namespace fmCommon.Battle
{
    public class fmPvpProcess : fmBattleProcess
    {
        int maxTurn = 10;

        protected override bool CheckEnd(int turn, fmReplay replay)
        {
            if (true == m_buOther.Surrender())
            {
                replay.Result = eBattleResult.Win;
                return true;
            }

            if (true == m_buMy.Surrender())
            {
                replay.Result = eBattleResult.Lose;
                return true;
            }

            // pvp 최대 10턴
            if (maxTurn <= turn)
            {
                replay.Result = eBattleResult.Lose;
                return true;
            }

            return false;
        }

        protected override void Release()
        {
            if (null != m_buMy)
            {
                m_buMy.Dispose();
                m_buMy = null;
            }
            if (null != m_buOther)
            {
                m_buOther.Dispose();
                m_buOther = null;
            }
        }
    }
}
