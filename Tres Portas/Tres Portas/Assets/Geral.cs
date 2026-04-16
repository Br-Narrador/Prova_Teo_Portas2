using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Geral : MonoBehaviour
{
    public Text controle, experimento, instrucao;
    bool ajuda = false;
    bool esq, dir, cent, pTentativa;
    float taxaCAjuda, taxaSAjuda;
    float acertoCAjuda = 0, acertoSAjuda = 0, tentativasSAjuda = 0, tentativasCAjuda = 0;
    void Start()
    {
        Sortear();
    }
    void Sortear() {
        int sort = Random.Range(0, 100);
        if (sort <= 33)
        {
            esq = true; dir = false; cent = false;
            print("esq");
        }
        else if (sort <= 66)
        {
            cent = true; esq = false; dir = false;
            print("cent");
        }
        else
        {
            dir = true; esq = false; cent = false;
            print("dir");
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
                acertoSAjuda++;
                Sortear();
                tentativasSAjuda++;
                instrucao.text = "Parabens, acertou, tente de novo";

            }
            else if (!esq)
            {
                tentativasSAjuda++;
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
                    acertoCAjuda++;
                    Sortear();
                    tentativasCAjuda++;
                    instrucao.text = "Parabens, acertou, tente de novo";

                }
                else if (!esq)
                {
                    print("rodei");
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
                acertoSAjuda++;
                Sortear();
                tentativasSAjuda++;
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
                    acertoCAjuda++;
                    Sortear();
                    tentativasCAjuda++;
                    instrucao.text = "Parabens, acertou, tente de novo";

                }
                else if (!dir)
                {
                    tentativasCAjuda++;
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
                acertoSAjuda++;
                Sortear();
                tentativasSAjuda++;
                instrucao.text = "Parabens, acertou, tente de novo";
            }
            else if (!cent)
            {
                tentativasSAjuda++;
                Sortear();
                instrucao.text = "Voce errou";
            }
        }
        //parte com ajuda
        if (ajuda)
        {
            print("vou tentar");
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
                print("consegui");
                    if (cent)
                    {
                        acertoCAjuda++;
                        Sortear();
                        tentativasCAjuda++;
                        instrucao.text = "Parabens, acertou, tente de novo";

                    }
                    else if (!cent)
                    {
                        tentativasCAjuda++;
                        Sortear();
                        instrucao.text = "Voce errou";
                    
                    }
            }
        }
    }
    private void Update()
    {
        controle.text = "Taxa sem ajuda: " + acertoSAjuda + "/" + tentativasSAjuda;
        experimento.text = "Taxa com ajuda: " + acertoCAjuda + "/" + tentativasCAjuda;
    }
}
