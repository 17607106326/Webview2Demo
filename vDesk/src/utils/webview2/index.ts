
/**
 * 解决 window?.WV2 报错
 */
declare const window: any;

/**
 * 对webview2暴露方法的封装
 * @param callName 方法名
 * @param callParams 参数名
 * @param jsonStringify 是否需要序列化，针对对象数据需要序列化
 * @returns
 */
export function WV2Call(callName: string, callParams: any = '', jsonStringify = true) {
  // 将参数转换为字符串，webview2后台只能接受字符串格式
  const params = callParams;
  if (jsonStringify) {
    const params = JSON.stringify(callParams);
  }
  /** webview2的客户端对象，方便全局获取使用 */
  const $WV2 = window?.WV2?.mainCall;

  return new Promise<WV2Model>(async (resolve, reject) => {
    $WV2.Call(callName, params)
      .then((res: any) => {
        resolve(JSON.parse(res) as WV2Model);
      }).catch((e: Error) => {
        reject(e);
      });
  })
}

/**
 * webview 的返回值对象接口
 */
export interface WV2Model {
  /**
   * 返回值编码 （-1：失败，200：成功）
   */
  Code: number;
  /**
   * 返回消息
   */
  Msg: string;
  /**
   * 返回数据
   */
  Data: any;
}
