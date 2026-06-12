using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class DadosPontuacao
{
    public string coluna; // Mude para 'coluna' se no seu JS estiver em português
}

public class ConexaoAPI : MonoBehaviour
{
    private string urlSalvar = "http://172.19.115.1:3000/pontos/atualizar";

    // Você vai chamar essa função de fora sempre que o jogo acabar!
    // Exemplo: EncontrarObjetoComEsseScript.RegistrarFimDePartida("acertosCAjuda");
    public void RegistrarFimDePartida(string nomeDaColuna)
    {
        StartCoroutine(RegistrarPonto(nomeDaColuna));
    }

    public IEnumerator RegistrarPonto(string nomeDaColuna)
    {
        print(nomeDaColuna);
        DadosPontuacao novoPonto = new DadosPontuacao();
        novoPonto.coluna = nomeDaColuna; // ou novoPonto.coluna, ajuste conforme sua API

        string jsonDados = JsonUtility.ToJson(novoPonto);

        using (UnityWebRequest request = new UnityWebRequest(urlSalvar, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonDados);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Erro ao salvar ponto: " + request.error);
            }
            else
            {
                Debug.Log("Ponto registrado na API!");

                // DICA DE OURO: Assim que a API avisar que salvou com sucesso,
                // nós podemos mandar o script de UI atualizar a tela automaticamente!
                GerenciadorUI ui = FindObjectOfType<GerenciadorUI>();
                if (ui != null)
                {
                    ui.Start(); // Força o script da UI a rodar a busca de novo para atualizar os números
                }
            }
        }
    }
}