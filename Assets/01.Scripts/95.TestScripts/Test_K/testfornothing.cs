using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class testfornothing : MonoBehaviour
{

    [SerializeField] Button btn;
    [SerializeField] Image panel;
    [SerializeField] int i;
    async void Start()
    {
        btn.onClick.AddListener(async () =>
         {
            switch (i)
            {
                case 1: // 직접 어드레서블 로드 
                    panel.sprite = await Addressables.LoadAssetAsync<Sprite>("Test").Task;
                    break;
                case 2: // 커스텀 로드 사용
                    panel.sprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>("Test");
                    break;
                case 3: // 리소스 로드 사용
                    panel.sprite = Resources.Load<Sprite>("Test");
                    break;
                case 4: // 직접 어드레서블 로드 x5
                    panel.sprite = await Addressables.LoadAssetAsync<Sprite>("Test").Task;
                    panel.sprite = await Addressables.LoadAssetAsync<Sprite>("Test").Task;
                    panel.sprite = await Addressables.LoadAssetAsync<Sprite>("Test").Task;
                    panel.sprite = await Addressables.LoadAssetAsync<Sprite>("Test").Task;
                    panel.sprite = await Addressables.LoadAssetAsync<Sprite>("Test").Task;
                    break;
                case 5: // 커스텀 로드 사용 x5
                    panel.sprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>("Test");
                    panel.sprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>("Test");
                    panel.sprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>("Test");
                    panel.sprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>("Test");
                    panel.sprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>("Test");
                    break;
                case 6: // 리소스 로드 사용 x5
                    panel.sprite = Resources.Load<Sprite>("Test");
                    panel.sprite = Resources.Load<Sprite>("Test");
                    panel.sprite = Resources.Load<Sprite>("Test");
                    panel.sprite = Resources.Load<Sprite>("Test");
                    panel.sprite = Resources.Load<Sprite>("Test");
                    break;
            }
        });
    }
}
