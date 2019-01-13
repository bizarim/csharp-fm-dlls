using System;
using System.Collections.Generic;

namespace fmCommon.Battle.Base
{
    public interface IBattleUnit : IDisposable
    {
        // 초기화
        bool Initialize();
        // 크리 결정
        bool IsCritical();
        // 공격 방식 결정
        eActType GetInAtkType();
        bool TryGetPreActType(out List<eActType> list);
        bool TryGetPostActType(out List<eActType> list);
        // 무기 속성 얻기
        eElement GetElement(eActType act = eActType.Nomal);
        // 현재 생명력 얻기
        long GetCurHp();
        // 현재 레벨 얻기
        int GetLv();
        eRareLv GetRareLv();
        // 현재 공격력 얻기
        long WDPS(bool isCri, eRareLv rareLv);
        long EDPS(bool isCri, eRareLv rareLv);

        long Thorn(long damage, int lv);
        long Poison();
        long Burn(bool isCri);
        long Freeze(bool isCri);
        // 크러싱블로우 당했을 때
        long BeAtkCrushingBlow(bool isCri);
        // 방어 데미지 감소 계산
        long DefendWD(eActType actType, long damage, int lv, eElement element);
        long DefendED(eActType actType, long damage, int lv, eElement element);
        // 데미지 적용
        void TakeDamge(long damage);
        // 체크
        bool Surrender();
        // 회복
        int Recovery();
        //int IceRecovery();
        int GetExtraCnt();
        void SetAbnormal(eAbnormal eAb);
        eAbnormal GetAbnormal();
    }
}
