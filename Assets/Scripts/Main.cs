using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using XFABManager;
using XFGameFramework;

public class Main : MonoBehaviour
{
    [SerializeField]
    private StartupPanel startupPanel;
    [SerializeField]
    private TipPanel tipPanel;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        //������Դ
        yield return StartCoroutine(ReadyMainModuleRes());

        // ����MainModule
        StartUpModuleRequest request = ModuleManager.StartUpModule<MainModule>();
        while (!request.isDone)
        {
            yield return null;
            LogUtil.Log("ģ���������� {0}", request.progress);
        }
        //yield return request;
        if (!string.IsNullOrEmpty(request.error))
            Debug.LogErrorFormat("ģ������ʧ��:{0}", request.error);
    }

    /// <summary>
    /// ׼��MainModule��Դ
    /// </summary>
    /// <returns></returns>
    IEnumerator ReadyMainModuleRes()
    {
        //��ѭ������:�����Դ׼��ʧ��������׼��,����ɹ�������ѭ��
        while (true)
        {
            // �����Դ
            CheckResUpdateRequest request_check = AssetBundleManager.CheckResUpdate("MainModule");

            yield return request_check;

            if (string.IsNullOrEmpty(request_check.error))
            {
                // ��Դ���ɹ� 
                if (request_check.result.updateType == UpdateType.DontNeedUpdate) break;
                // �����Ҫ����,������Ը��ݸ�����������һЩ��ʾ,
                // ����:һ������»��ڸ���֮ǰ��ʾ�û�,��Ҫ���ض�����Դ,��������,����Ϣ TODO 
                // �������Ҫ��ʾ�Ļ����������κδ���
                if (request_check.result.updateSize > 0)
                {
                    string updateSize = StringTools.FormatByte(request_check.result.updateSize);
                    string content = string.Format("��⵽����Դ��Ҫ���£�\n �汾:{0} ��Դ��С:{1}", request_check.result.version, updateSize);
                    tipPanel.ShowTip("������ʾ", content, () => tipPanel.Hide(), "������Դ", true, () => Application.Quit(), "�˳���Ϸ");
                    //�ȴ��û��������
                    while (tipPanel.gameObject.activeSelf)
                        yield return null;
                }
            }
            else
            {
                // ��Դ���ʧ��,������Ӧ��ʾ��,�ٴμ��
                string updateSize = StringTools.FormatByte(request_check.result.updateSize);
                string content = string.Format("��Դ���ʧ�ܣ�������������ԣ�");
                tipPanel.ShowTip("������ʾ", content, () => tipPanel.Hide(), "������Դ", true, () => Application.Quit(), "�˳���Ϸ");
                //�ȴ��û��������
                while (tipPanel.gameObject.activeSelf)
                    yield return null;
                continue;
            }

            // ׼����Դ
            ReadyResRequest request = AssetBundleManager.ReadyRes(request_check.result);

            while (!request.isDone)
            {
                yield return null;
                // �������������UI���� TODO
                switch (request.ExecutionType)
                {
                    case ExecutionType.Download:
                        // ����������Դ
                        string totalSize = StringTools.FormatByte(request.NeedDownloadedSize);
                        string downloadSize = StringTools.FormatByte(request.DownloadedSize);
                        string speed = StringTools.FormatByte(request.CurrentSpeed);
                        string message = string.Format("<color=#00FFFF>������...</color>{0}/{1}", downloadSize, totalSize);
                        startupPanel.SetMessage(message);
                        break;
                    case ExecutionType.Decompression:
                        // ��ѹ��Դ
                        startupPanel.SetMessage("<color=#00FFFF>���ڽ�ѹ��Դ...</color>");
                        break;
                    case ExecutionType.Verify:
                        // У����Դ(��Դ������ɺ�,��ҪУ���ļ��Ƿ���)
                        startupPanel.SetMessage("<color=#00FFFF>����У����Դ...</color>");
                        break;
                    case ExecutionType.ExtractLocal:
                        // �ͷ���Դ(����Դ������Ŀ¼���Ƶ�����Ŀ¼)
                        startupPanel.SetMessage("<color=#00FFFF>�����ͷ���Դ...</color>");
                        break;
                }
                startupPanel.SetProgress(request.progress);
            }

            if (string.IsNullOrEmpty(request.error))
            {
                // ��Դ׼���ɹ�����ѭ��
                LogUtil.Log("��Դ׼���ɹ�");
                break;
            }
            else
            {
                LogUtil.LogError("��Դ׼��ʧ��", request.error);
                // ��Դ׼��ʧ��,������Ӧ��ʾ,Ȼ���ٴ�׼�� TODO
                string updateSize = StringTools.FormatByte(request_check.result.updateSize);
                string content = string.Format("��Դ׼��ʧ�ܣ�������������ԣ�");
                tipPanel.ShowTip("������ʾ", content, () => tipPanel.Hide(), "������Դ", true, () => Application.Quit(), "�˳���Ϸ");
                //�ȴ��û��������
                while (tipPanel.gameObject.activeSelf)
                    yield return null;

                yield return new WaitForSeconds(2);
            }
        }
    }
}
