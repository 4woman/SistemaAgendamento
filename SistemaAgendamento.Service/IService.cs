namespace SistemaAgendamento.Service
{
    public interface IServive<T>
    {
        int Cadastrar(T model);
        bool Editar(T model);
        List<T> Listar();
        T ListarPorId(int id);
        bool Deletar(int id);

    }
}
