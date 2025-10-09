using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendamentoSistema.Models;
using SistemaAgendamento.Models;

namespace SistemaAgendamento.Service
{
    public class AgendamentoService : BaseService<Agendamento>
    {
        public bool VerificarDisponibilidade(Agendamento agendamento)
        {
            return Listar().Any(a =>
            a.ProfissionalId == agendamento.ProfissionalId &&
            a.DataHoraInicio < agendamento.DataHoraFim &&
            a.DataHoraFim > agendamento.DataHoraInicio);
        }

        public int Agendar(Agendamento agendamento)
        {
            if (VerificarDisponibilidade(agendamento))
                throw new Exception("Horário indisponivel para o profissional");
            return Cadastrar(agendamento);
        }

        public bool CancelarAgendamento(int id)
        {
            Agendamento agendamento = ListarPorId(id);
            if (agendamento == null) return false;
            agendamento.Status = StatusPedido.Cancelado;
            return Editar(agendamento);
        }

        public List<Agendamento> ListarPorUsuario(int usuarioId)
        {
            return Listar().Where(a => a.UsuarioId == usuarioId).ToList();
        }

        public List<Agendamento> ListarPorProfissional(int profissionalId)
        {
            return Listar().Where(a => a.ProfissionalId == profissionalId).ToList();
        }


    }
}
