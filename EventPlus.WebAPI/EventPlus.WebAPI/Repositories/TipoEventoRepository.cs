using EventPlus.WebAPI.BdContextEvet;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories
{
    public class TipoEventoRepository :
         ITipoEventoRepository
    {
        private readonly EventContext _context;

        //Injecao de dependência
        public TipoEventoRepository(EventContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Atualiza um tipo de evento usando o rastreamento automatico
        /// </summary>
        /// <param name="id">id do tipo evento a ser atualizado</param>
        /// <param name="tipoEvento">Novos dados do tipo evento</param>

        public void Atualizar(Guid id, TipoEvento tipoEvento)
        {
            var TipoEventoBuscado = _context.TipoEventos.Find(id);

            if (TipoEventoBuscado != null)
            {
                TipoEventoBuscado.Titulo = tipoEvento.Titulo;
                //o savachanges () detecta a mudanca na propriedade "titulo" automatico
                _context.SaveChanges();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id do tipo evento a ser buscado</param>
        /// <returns>Objeto do TipoEvento com as informacoes do tipo de evento buscado</returns>
        public TipoEvento BuscarPorId(Guid id)
        {
            return _context.TipoEventos.Find(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipoEvento">tipo de evento a ser cadastrado</param>
        public void Cadastrar(TipoEvento tipoEvento)
        {
            _context.TipoEventos.Add(tipoEvento);
            _context.SaveChanges();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id do tipo evento a ser deletado</param>
        public void Deletar(Guid id)
        {
           var tipoEventoBuscado = _context.TipoEventos.Find(id);

            if (tipoEventoBuscado != null)
            {
                _context.TipoEventos.Remove(tipoEventoBuscado);
                _context.SaveChanges();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Uma lista de tipo eventos</returns>
        public List<TipoEvento> Listar()
        {
           return _context.TipoEventos.OrderBy(tipoEvento => tipoEvento.Titulo).ToList();
        }

    }
}

