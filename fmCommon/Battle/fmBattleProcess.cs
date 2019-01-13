using fmCommon.Battle.Base;
using System.Collections.Generic;

namespace fmCommon.Battle
{
    enum eCombatant { My, Other }

    public class fmBattleProcess : IBattleProcess
    {

        int maxTurn = 50;

        protected override bool Initialize()
        {
            if (null == m_buMy) return false;
            if (null == m_buOther) return false;

            if (false == m_buMy.Initialize()) return false;
            if (false == m_buOther.Initialize()) return false;

            return true;
        }

        protected override fmReplay DoBattle()
        {
            fmReplay replay = new fmReplay();
            replay.MyStartHp = m_buMy.GetCurHp();
            replay.OtherStartHp = m_buOther.GetCurHp();
            replay.MyElement = m_buMy.GetElement();
            replay.OtherElement = m_buOther.GetElement();

            int turn = 0;

            while (true)
            {
                // 1. 전투 종료 검사
                if (CheckEnd(turn, replay)) break;
                // 2. 턴 증가
                ++turn;
                // 4. Lv 얻어오기
                // 5. 속성 얻어오기
                fmReplayNode replayNode = new fmReplayNode();

                // my PreAct
                ProcPreAct(eCombatant.My, m_buMy, m_buOther, replayNode);
                // other PreAct
                ProcPreAct(eCombatant.Other, m_buOther, m_buMy, replayNode);

                // other Clear eAb
                ProcClearAbnormal(eCombatant.Other);

                // my InAct
                ProcMyInAct(replayNode);

                // my Clear eAb
                ProcClearAbnormal(eCombatant.My);

                // other InAct
                ProcOtherInAct(replayNode);

                // my PostAct
                ProcPostAct(eCombatant.My, m_buMy, m_buOther, replayNode);
                // other PostAct
                ProcPostAct(eCombatant.Other, m_buOther, m_buMy, replayNode);

                // 11. 턴 / 남은 량 기록
                replayNode.Turn = turn;
                replayNode.MyRemainHp = m_buMy.GetCurHp();
                replayNode.OtherRemainHp = m_buOther.GetCurHp();

                // 12. 리플레이 추가.
                replay.TryAdd(replayNode);
            }

            return replay;
        }

        private bool TryPreAct(IBattleUnit attacker, IBattleUnit defender, eActType atkType, out fmActNode node)
        {
            node = null;
            long value = 0;
            long ed = 0;

            m_dicFn[atkType](attacker, defender, false, out value, out ed);

            if (value <= 0)
                return false;

            node = new fmActNode
            {
                ActType = atkType,
                IsCri = false,
                Value = value,
                WD = 0,
                ED = 0
            };

            return true;
        }

        private bool TryInAct(IBattleUnit attacker, IBattleUnit defender, eActType atkType, out fmActNode node, long inputDPS = 0)
        {
            node = null;

            int lv = attacker.GetLv();
            eElement element = attacker.GetElement();
            long wd = 0;
            long ed = 0;
            bool cri = attacker.IsCritical();                       // 6. 크리티컬 결정

            m_dicFn[atkType](attacker, defender, cri, out wd, out ed, inputDPS);     // 8. 데미지 얻어오기

            long rsWD = defender.DefendWD(atkType, wd, lv, element);        // 9. 데미지 감소 계산 하기
            long rsED = defender.DefendED(atkType, ed, lv, element);        // 9. 데미지 감소 계산 하기

            long rsDamage = rsWD + rsED;

            if (0 < attacker.GetCurHp()) defender.TakeDamge(rsDamage);   // 10. 데미지 적용
            else rsDamage = 0;

            if (rsDamage <= 0)
                return false;

            node = new fmActNode
            {
                ActType = atkType,
                IsCri = cri,
                Value = rsDamage,
                WD = rsWD,
                ED = rsED
            };

            return true;
        }

        private bool TryPostAct(IBattleUnit attacker, IBattleUnit defender, eActType atkType, out fmActNode node)
        {
            node = null;

            int lv = attacker.GetLv();
            eElement element = attacker.GetElement(atkType);
            long wd = 0;
            long ed = 0;
            bool cri = attacker.IsCritical();                       // 6. 크리티컬 결정

            m_dicFn[atkType](attacker, defender, cri, out wd, out ed);     // 8. 데미지 얻어오기

            long rsWD = defender.DefendWD(atkType, wd, lv, element);        // 9. 데미지 감소 계산 하기
            long rsED = defender.DefendED(atkType, ed, lv, element);        // 9. 데미지 감소 계산 하기

            long rsDamage = rsWD + rsED;

            if (0 < attacker.GetCurHp()) defender.TakeDamge(rsDamage);   // 10. 데미지 적용
            else rsDamage = 0;

            if (rsDamage <= 0)
                return false;

            node = new fmActNode
            {
                ActType = atkType,
                IsCri = false,
                Value = rsDamage,
                WD = rsWD,
                ED = rsED
            };

            return true;
        }


        private void ProcClearAbnormal(eCombatant combatant)
        {
            if (combatant == eCombatant.My)
                m_buMy.SetAbnormal(eAbnormal.None);
            else
                m_buOther.SetAbnormal(eAbnormal.None);
        }

        private void ProcSetAbnormal(eCombatant combatant, eActType eAct)
        {
            if (eAct == eActType.None)
                return;

            eAbnormal eAb = eAbnormal.None;

            if (eAct == eActType.Sturn)
                eAb = eAbnormal.Sturn;
            else if (eAct == eActType.Freeze)
                eAb = eAbnormal.Freeze;

            if (combatant == eCombatant.My)
                m_buMy.SetAbnormal(eAb);
            else
                m_buOther.SetAbnormal(eAb);

        }

        private void ProcPreAct(eCombatant combatant, IBattleUnit attacker, IBattleUnit defender, fmReplayNode replayNode)
        {
            if (eAbnormal.None != attacker.GetAbnormal())
                return;

            List<eActType> preActList = null;
            attacker.TryGetPreActType(out preActList);

            foreach (var node in preActList)
            {
                fmActNode actNode = null;
                if (true == TryPreAct(attacker, defender, node, out actNode))
                {
                    if(combatant == eCombatant.My)
                        replayNode.TryMyPreAdd(actNode);
                    else
                        replayNode.TryOtherPreAdd(actNode);
                }
            }
        }

        private void ProcInAct(eCombatant combatant, IBattleUnit attacker, IBattleUnit defender, eActType actType, fmReplayNode replayNode)
        {
            if (eAbnormal.None != attacker.GetAbnormal())
                return;

            if (actType == eActType.None)
                return;

            fmActNode actNode = null;
            if (true == TryInAct(attacker, defender, actType, out actNode))
            {
                if (combatant == eCombatant.My)
                    replayNode.TryMyInAdd(actNode);
                else
                    replayNode.TryOtherInAdd(actNode);

                fmActNode otherActNode = null;
                if (true == TryInAct(defender, attacker, eActType.Thorn, out otherActNode, actNode.WD))
                {
                    if (combatant == eCombatant.My)
                        replayNode.TryOtherPostAdd(otherActNode);
                    else
                        replayNode.TryMyPostAdd(otherActNode);

                }
            }
        }

        private void ProcPostAct(eCombatant combatant, IBattleUnit attacker, IBattleUnit defender, fmReplayNode replayNode)
        {
            if (eAbnormal.None != attacker.GetAbnormal())
                return;

            List<eActType> postAtkList = null;
            attacker.TryGetPostActType(out postAtkList);

            foreach (var node in postAtkList)
            {
                fmActNode actNode = null;
                if (true == TryPostAct(attacker, defender, node, out actNode))
                {
                    if (combatant == eCombatant.My)
                        replayNode.TryMyPostAdd(actNode);
                    else
                        replayNode.TryOtherPostAdd(actNode);
                }
            }
        }


        private void ProcMyInAct(fmReplayNode replayNode)
        {
            // 7. 공격 방식 결정하기
            eActType actType = m_buMy.GetInAtkType();
            if (actType == eActType.ExtraAtk)
            {
                ProcInAct(eCombatant.My, m_buMy, m_buOther, eActType.Nomal, replayNode);

                for (int i = 0; i < m_buMy.GetExtraCnt(); ++i)
                {
                    ProcInAct(eCombatant.My, m_buMy, m_buOther, actType, replayNode);
                }
            }
            else
            {
                ProcSetAbnormal(eCombatant.Other, actType);
                ProcInAct(eCombatant.My, m_buMy, m_buOther, actType, replayNode);
            }
        }

        private void ProcOtherInAct(fmReplayNode replayNode)
        {
            eActType actType = m_buOther.GetInAtkType();
            if (actType == eActType.ExtraAtk)
            {
                ProcInAct(eCombatant.Other, m_buOther, m_buMy, eActType.Nomal, replayNode);

                for (int i = 0; i < m_buOther.GetExtraCnt(); ++i)
                {
                    ProcInAct(eCombatant.Other, m_buOther, m_buMy, actType, replayNode);
                }
            }
            else
            {
                ProcSetAbnormal(eCombatant.My, actType);
                ProcInAct(eCombatant.Other, m_buOther, m_buMy, actType, replayNode);
            }
        }


        protected virtual bool CheckEnd(int turn, fmReplay replay)
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

            if (maxTurn <= turn)
            {
                replay.Result = eBattleResult.Lose;
                return true;
            }

            return false;
        }
    }
}
