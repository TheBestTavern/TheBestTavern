public class InventoryViewFixed : InventoryView
{

    public override void 아이템띄우기()  // 전체 아이템 띄우기
    {
        int targetIndex = 0;
        foreach (var pair in controller.모델정보반환())
        {
            index2Slots[targetIndex].슬롯세팅(pair.Key);
            BiID2SlotIndex.Add(pair.Key, targetIndex);
            targetIndex++;
        }
    }


    // 메인, 부엌
    public virtual void 아이템이동() { } // a슬롯의 정보를 b슬롯으로 이동
    public virtual void 아이템버리기() { } // 아이템 버리는 로직 ( 갯수 선택 팝업 )
}