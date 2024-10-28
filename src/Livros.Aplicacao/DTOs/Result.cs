using Flunt.Notifications;
using Livros.Dominio.Entidades;
using Livros.Dominio.Enumeracoes;
using System.Text.Json.Serialization;

namespace Livros.Aplicacao.DTOs;

public class Result<T> : Notifiable<Notification> where T : class
{
    public T Data { get; set; }

    /// <summary>
    /// Armazena o resumo da ação realizada pelo serviço da aplicação
    /// </summary>
    /// <remarks>Utilizada para dar contexto e auxiliar no retorno dos endpoints 
    /// (em caso de sistema web) ou outras tecnologias que utilizem esses serviços
    /// da aplicação</remarks>
    [JsonIgnore]
    public EResultadoAcaoServico ResultadoAcao { get; private set; }

    public void AddResultadoAcao(EResultadoAcaoServico resultadoAcao)
    {
        ResultadoAcao = resultadoAcao;
    }
}