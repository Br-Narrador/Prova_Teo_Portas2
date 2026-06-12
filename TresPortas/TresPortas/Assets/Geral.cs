using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Geral : MonoBehaviour
{
    public Text controle, experimento, instrucao, controleGlobal, experimentoGlobal;
    bool ajuda = false;
    bool esq, dir, cent, pTentativa;
    float acertosCAjuda = 0, acertosSAjuda = 0, tentativasSAjuda = 0, tentativasCAjuda = 0;
    ConexaoAPI conexaoAPI;
    void Start()
    {
        Sortear();
        conexaoAPI = GetComponent<ConexaoAPI>();
    }
    void Sortear() {
        int sort = Random.Range(0, 100);
        if (sort <= 33)
        {
            esq = true; dir = false; cent = false;
        }
        else if (sort <= 66)
        {
            cent = true; esq = false; dir = false;
        }
        else
        {
            dir = true; esq = false; cent = false;
        }
        pTentativa = true;
    }

    public void TrocaDeModo() {
        ajuda = !ajuda;
    }

    public void Esq(){
        //parte sem ajuda
        if (!ajuda)
        {
            if (esq)
            {
                acertosSAjuda++;
                StartCoroutine(conexaoAPI.RegistrarPonto("acertosSAjuda"));
                Sortear();
                tentativasSAjuda++;
                StartCoroutine(conexaoAPI.RegistrarPonto("tentativasSAjuda"));
                instrucao.text = "Parabens, acertou, tente de novo";

            }
            else if (!esq)
            {
                tentativasSAjuda++;
                StartCoroutine(conexaoAPI.RegistrarPonto("tentativasSAjuda"));
                Sortear();
                instrucao.text = "Voce errou";
            }
        }
        //parte com ajuda
        if (ajuda)
        {
            if (esq && pTentativa)
            {
                int sort = Random.Range(0, 2);
                if (sort < 1)
                {
                    instrucao.text = "A porta direita certamente nao e a certa";
                }
                else instrucao.text = "A porta do meio definitivamente nao e a certa";
                pTentativa = false;
            }
            else if (!esq && pTentativa)
            {
                if (dir) instrucao.text = "A porta do meio definitivamente nao e a certa";
                else instrucao.text = "A porta da direita certamente nao e a certa";
                pTentativa = false;
            }
            else if(!pTentativa)
            {
                if (esq)
                {
                    print("rodei");
                    acertosCAjuda++;
                    StartCoroutine(conexaoAPI.RegistrarPonto("acertosCAjuda"));
                    Sortear();
                    tentativasCAjuda++;
                    StartCoroutine(conexaoAPI.RegistrarPonto("tentativasCAjuda"));
                    instrucao.text = "Parabens, acertou, tente de novo";

                }
                else if (!esq)
                {
                    print("rodei");
                    StartCoroutine(conexaoAPI.RegistrarPonto("tentativasCAjuda"));
                    tentativasCAjuda++;
                    Sortear();
                    instrucao.text = "Voce errou";
                }
            }
        }
    }
    public void Dir()
    {
        //parte sem ajuda
        if (!ajuda)
        {
            if (dir)
            {
                acertosSAjuda++;
                StartCoroutine(conexaoAPI.RegistrarPonto("acertosSAjuda"));
                Sortear();
                tentativasSAjuda++;
                StartCoroutine(conexaoAPI.RegistrarPonto("tentativasSAjuda"));
                instrucao.text = "Parabens, acertou, tente de novo";
            }
            else if (!dir)
            {
                tentativasSAjuda++;
                Sortear();
                instrucao.text = "Voce errou";
            }
        }
        //parte com ajuda
        if (ajuda)
        {
            if (pTentativa)
            {
                if (dir && pTentativa)
                {
                    int sort = Random.Range(0, 2);
                    if (sort < 1)
                    {
                        instrucao.text = "A porta esquerda certamente nao e a certa";
                    }
                    else instrucao.text = "A porta do meio definitivamente nao e a certa";
                }
                else if (!dir && pTentativa)
                {
                    if (esq) instrucao.text = "A porta do meio definitivamente nao e a certa";
                    else instrucao.text = "A porta esquerda certamente nao e a certa";
                }
                pTentativa = false;
            }
            else
            {
                if (dir)
                {
                    acertosCAjuda++;
                    StartCoroutine(conexaoAPI.RegistrarPonto("acertosCAjuda"));
                    Sortear();
                    tentativasCAjuda++;
                    StartCoroutine(conexaoAPI.RegistrarPonto("tentativasCAjuda"));
                    instrucao.text = "Parabens, acertou, tente de novo";

                }
                else if (!dir)
                {
                    tentativasCAjuda++;
                    StartCoroutine(conexaoAPI.RegistrarPonto("tentativasCAjuda"));
                    Sortear();
                    instrucao.text = "Voce errou";
                }
            }
        }
    }
    public void Cent()
    {
        //parte sem ajuda
        if (!ajuda)
        {
            if (cent)
            {
                acertosSAjuda++;
                StartCoroutine(conexaoAPI.RegistrarPonto("acertosSAjuda"));
                Sortear();
                tentativasSAjuda++;
                StartCoroutine(conexaoAPI.RegistrarPonto("tentativasSAjuda"));
                instrucao.text = "Parabens, acertou, tente de novo";
            }
            else if (!cent)
            {
                tentativasSAjuda++;
                StartCoroutine(conexaoAPI.RegistrarPonto("tentativasSAjuda"));
                Sortear();
                instrucao.text = "Voce errou";
            }
        }
        //parte com ajuda
        if (ajuda)
        {
            if (pTentativa)
            {
                if (cent && pTentativa)
                {
                    int sort = Random.Range(0, 2);
                    if (sort < 1)
                    {
                        instrucao.text = "A porta direita certamente nao e a certa";
                    }
                    else instrucao.text = "A porta esquerda certamente nao e a certa";                 
                }
                else if (!cent && pTentativa)
                {
                    if (esq) instrucao.text = "A porta direita certamente nao e a certa";
                    else instrucao.text = "A porta esquerda certamente nao e a certa";                  
                }
                pTentativa = false;
            }
            else {
                    if (cent)
                    {
                        acertosCAjuda++;
                        StartCoroutine(conexaoAPI.RegistrarPonto("acertosCAjuda"));
                        Sortear();
                        tentativasCAjuda++;
                        StartCoroutine(conexaoAPI.RegistrarPonto("tentativasCAjuda"));
                        instrucao.text = "Parabens, acertou, tente de novo";

                    }
                    else if (!cent)
                    {
                        tentativasCAjuda++;
                        StartCoroutine(conexaoAPI.RegistrarPonto("tentativasCAjuda"));
                        Sortear();
                        instrucao.text = "Voce errou";
                    
                    }
            }
        }
    }
    private void Update()
    {

        controle.text = "Taxa sem ajuda: " + acertosSAjuda + "/" + tentativasSAjuda;
        experimento.text = "Taxa com ajuda: " + acertosCAjuda + "/" + tentativasCAjuda;
    }
}
