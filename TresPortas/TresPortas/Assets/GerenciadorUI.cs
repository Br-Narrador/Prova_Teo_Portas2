using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// 1. Criamos uma classe que representa UMA linha da tabela do banco
// OS NOMES DAS VARIÁVEIS PRECISAM SER IDÊNTICOS AOS DO BANCO DE DADOS!
[System.Serializable]
public class LinhaPlacar
{
    public int id;
    public int acertosCAjuda;
    public int acertosSAjuda;
    public int tentativasSAjuda;
    public int tentativasCAjuda;
}

public class GerenciadorUI : MonoBehaviour
{
    private string urlBuscar = "http://172.19.115.1:3000/pontos";

    // 2. Arraste os seus elementos de texto da UI aqui pelo Inspector da Unity
    [Header("Componentes de UI (TextMeshPro)")]
    public Text experimentoGlobaltxt;
    public Text controleGlobaltxt;

    public void Start()
    {
        // Busca os dados assim que o jogo inicia para atualizar a tela
        StartCoroutine(AtualizarPlacarUI());
    }

    IEnumerator AtualizarPlacarUI()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(urlBuscar))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonRecebido = webRequest.downloadHandler.text;
                Debug.Log("JSON puro vindo da API: " + jsonRecebido);

                // Como o banco responde uma LISTA de linhas (dentro de colchetes [ ]),
                // nós precisamos "ajudar" a Unity a entender que queremos a primeira linha (índice 0).
                // Para isso, gambitaremos envolvendo o JSON em uma estrutura de Array.
                string novoJson = "{\"linhas\":" + jsonRecebido + "}";

                // Converte o texto JSON em objetos que o C# entende
                ListaPlacar lista = JsonUtility.FromJson<ListaPlacar>(novoJson);

                if (lista.linhas.Length > 0)
                {
                    // Pegamos a primeira linha (id = 1)
                    LinhaPlacar placarGlobal = lista.linhas[0];

                    // 3. A MÁGICA ACONTECE AQUI: Pegamos cada coluna individualmente!
                    int acertosCA = placarGlobal.acertosCAjuda;
                    int acertosSA = placarGlobal.acertosSAjuda;
                    int tentativasCA = placarGlobal.tentativasCAjuda;
                    int tentativasSA = placarGlobal.tentativasSAjuda;

                    // 4. Jogamos os valores individuais diretamente na UI da Unity (.ToString() converte int para texto)
                    experimentoGlobaltxt.text ="Taxa Global Com Ajuda: " + acertosCA.ToString() + "/" + tentativasCA.ToString();
                    controleGlobaltxt.text = "Taxa Global Sem Ajuda: " + acertosSA.ToString() + "/" + tentativasSA.ToString();

                    Debug.Log("UI Atualizada com sucesso!");
                }
            }
            else
            {
                Debug.LogError("Erro ao buscar dados para a UI: " + webRequest.error);
            }
        }
    }
}

// Classe auxiliar necessária para a Unity conseguir ler Arrays/Listas de JSON
[System.Serializable]
public class ListaPlacar
{
    public LinhaPlacar[] linhas;
}