using System;
using System.Collections.Generic;

namespace fmCommon.Battle.Base
{
    public delegate void fmGetDamage(IBattleUnit attacker, IBattleUnit defender, bool isCri, out long WD, out long ED, long inputDPS = 0);

    public abstract class IBattleProcess : IDisposable
    {
        protected IBattleUnit m_buMy;
        protected IBattleUnit m_buOther;

        protected abstract bool Initialize();               // 부대 초기화
        //protected abstract void SetBuff();                  // 버프 적용
        protected abstract fmReplay DoBattle();               // 전투 공방

        public bool Process(IBattleUnit my, IBattleUnit other, out fmReplay replay)
        {
            replay = null;

            m_buMy = my;
            m_buOther = other;

            if (false == Initialize())
                return false;

            //SetBuff();
            replay = DoBattle();

            Release();

            return true;
        }

        protected virtual void Release()
        {
            m_buMy = null;
            m_buOther = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~IBattleProcess()
        {
            Dispose(false);
        }

        protected bool m_disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            //if (disposing)
            //{

            //}
            m_disposed = true;
        }


        protected Dictionary<eActType, fmGetDamage> m_dicFn = new Dictionary<eActType, fmGetDamage>();

        public IBattleProcess()
        {
            m_dicFn.Clear();
            m_dicFn.Add(eActType.None           , OnNone);
            m_dicFn.Add(eActType.Sturn          , OnSturn);
            m_dicFn.Add(eActType.Nomal          , OnNomalAttack);
            m_dicFn.Add(eActType.ExtraAtk       , OnExtraAttack);
            m_dicFn.Add(eActType.CrushingBlow   , OnCrushingBlow);
            m_dicFn.Add(eActType.Thorn          , OnThorn);
            m_dicFn.Add(eActType.Posion         , OnPosion);
            m_dicFn.Add(eActType.Recovery       , OnRecovery);
            //m_dicFn.Add(eActType.IceRecovery    , OnIceRecovery);                                      
            m_dicFn.Add(eActType.Burn           , OnBurn);
            m_dicFn.Add(eActType.Freeze         , OnFreeze);
        }

        protected void OnNomalAttack(IBattleUnit attacker, IBattleUnit defender, bool isCri, out long WD, out long ED, long inputDPS = 0)
        {
            WD = attacker.WDPS(isCri, defender.GetRareLv());
            ED = attacker.EDPS(isCri, defender.GetRareLv());
        }

        protected void OnExtraAttack(IBattleUnit attacker, IBattleUnit defender, bool isCri, out long WD, out long ED, long inputDPS = 0)
        {
            WD = attacker.WDPS(isCri, defender.GetRareLv());
            ED = attacker.EDPS(isCri, defender.GetRareLv());
        }

        protected void OnCrushingBlow(IBattleUnit attacker, IBattleUnit defender, bool isCri, out long WD, out long ED, long inputDPS = 0)
        {
            WD = defender.BeAtkCrushingBlow(isCri);
            ED = 0;
        }

        protected void OnThorn(IBattleUnit attacker, IBattleUnit defender, bool isCri, out long WD, out long ED, long inputDPS = 0)
        {
            //WD = attacker.WDPS(isCri, defender.GetRareLv());
            WD = 0;
            ED = attacker.Thorn(inputDPS, defender.GetLv());
        }

        protected void OnPosion(IBattleUnit attacker, IBattleUnit defender, bool isCri, out long WD, out long ED, long inputDPS = 0)
        {
            WD = 0;
            ED = attacker.Poison();
        }

        protected void OnRecovery(IBattleUnit attacker, IBattleUnit defender, bool isCri, out long recovery, out long ED, long inputDPS = 0)
        {
            recovery = attacker.Recovery();
            ED = 0;
        }

        protected void OnSturn(IBattleUnit attacker, IBattleUnit defender, bool isCri, out long WD, out long ED, long inputDPS = 0)
        {
            WD = attacker.WDPS(isCri, defender.GetRareLv());
            ED = attacker.EDPS(isCri, defender.GetRareLv());
        }

        protected void OnNone(IBattleUnit attacker, IBattleUnit defender, bool isCri, out long WD, out long ED, long inputDPS = 0)
        {
            WD = 0;
            ED = 0;
        }

        protected void OnBurn(IBattleUnit attacker, IBattleUnit defender, bool isCri, out long WD, out long ED, long inputDPS = 0)
        {
            WD = 0;
            ED = attacker.Burn(isCri);
        }

        protected void OnFreeze(IBattleUnit attacker, IBattleUnit defender, bool isCri, out long WD, out long ED, long inputDPS = 0)
        {
            WD = attacker.WDPS(isCri, defender.GetRareLv());
            ED = attacker.Freeze(isCri);
        }
    }
}
