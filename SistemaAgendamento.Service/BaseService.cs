using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaAgendamento.Models;

namespace SistemaAgendamento.Service
{
    public class BaseService<M> : IService<M> where M : BaseModel, new()
    {
        private static List<M> listas = new List<M>();
        private M modelOriginal = new M();
        public int Cadastrar(M model)
        {
            listas.Add(model);
            int id = model.Id;
            return id;
        }

        public bool Deletar(int id)
        {
            modelOriginal = ListarPorId(id);
            listas.Remove(modelOriginal);
            return true;
        }

        public bool Editar(M model)
        {
            modelOriginal = ListarPorId(model.Id);
            if (modelOriginal != null)
            {
                listas.Remove(modelOriginal);
                listas.Add(model);
                return true;
            }
            return false;
        }

        public List<M> Listar()
        {
            return listas;
        }

        public M ListarPorId(int id)
        {
            return listas.FirstOrDefault(x => x.Id == id);
        }
    }
}
