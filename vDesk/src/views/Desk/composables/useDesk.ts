import { WV2Call, WV2Model } from "@/utils/webview2";
import { onMounted, ref } from "vue"

export function useDesk(ExeInfos: any) {
  onMounted(() => {
    GetExeInfos();
  })

  const GetExeInfos = () => {
    WV2Call("Desk/GetInstallApplication")
      ?.then((res: WV2Model) => {
        ExeInfos.value = res.Data;
      })
      .catch((e: Error) => {
      });
  }

  const RunExe = () => {
    WV2Call("Desk/RunExe", 'C:/Users/17934/AppData/Local/360ChromeX/Chrome/Application/360ChromeX.exe', false)
      ?.then((res: WV2Model) => {
        ExeInfos.value = res.Data;
      })
      .catch((e: Error) => {
      });
  }

  return { RunExe }
}
