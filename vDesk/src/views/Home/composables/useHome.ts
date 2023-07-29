import { WV2Call, WV2Model } from "@/utils/webview2";
import { onMounted, ref } from "vue"

export function useHome() {
  onMounted(() => {
    // 初始化 hello 信息
    InitHello();
  })

  const HelloText = ref('');
  const InitHello = () => {
    WV2Call("Home/GetHelloText", "123")
      ?.then((res: WV2Model) => {
        HelloText.value = res.Data;
      })
      .catch((e: Error) => {
        debugger
      });
  }

  return { HelloText }
}
