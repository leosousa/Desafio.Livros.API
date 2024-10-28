using Flunt.Notifications;
using Livros.Dominio.Entidades;

namespace Livros.Aplicacao.DTOs;

public class Result<T> : Notifiable<Notification> where T : class
{
    public T Data { get; set; }
}