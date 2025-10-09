using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendamentoSistema.Models;

namespace SistemaAgendamento.Service
{
    public class HorarioDisponivelService : BaseService<HorarioDisponivel>
    {
        private readonly BaseService<Agendamento> _agendamentoService = new();
        public List<HorarioDisponivel> ListarPorProfissional(int profissionalId)
        {
            return Listar().Where(h => h.ProfissionalId == profissionalId).ToList();
        }

        public List<HorarioDisponivel> ListarDisponiveis(int profissionalId)
        {
            var horarios = ListarPorProfissional(profissionalId);

            return horarios.Where(h =>
                !_agendamentoService.Listar().Any(a =>
                    a.ProfissionalId == profissionalId &&
                    a.DataHoraInicio < h.DataHoraFim &&
                    a.DataHoraFim > h.DataHoraInicio
                )).ToList();
        }

        public bool OcupaHorario(int horarioId, Agendamento novoAgendamento)
        {
            var horario = ListarPorId(horarioId);
            if (horario == null) return false;

            // Verifica se o horário está livre
            var ocupado = _agendamentoService.Listar().Any(a =>
                a.ProfissionalId == horario.ProfissionalId &&
                a.DataHoraInicio < horario.DataHoraFim &&
                a.DataHoraFim > horario.DataHoraInicio);

            if (ocupado) return false;

            // Cadastra o agendamento
            _agendamentoService.Cadastrar(novoAgendamento);
            return true;
        }

    }
}
