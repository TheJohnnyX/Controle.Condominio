using MicroPromover.DTO;

namespace Promocao
{
    #region Interface
    public interface IServPromover
    {
        void Inserir(InserirPromoverDTO inserirPromoverDto);
        void Efetivar(int codigoPromover);
        void EfetivarAssincrono(int codigoPromover);
        void Editar(int id, EditarPromoverDTO editarPromoverDto);
        void Cancelar(int id);

        void ReceberResultInformarResultadoPromocaoAssincrono(ResultInformarPromoverPizza resultInformarPromoverPizza);
        string VerificarAndamentoProcessamento(int codigoPromover);

        List<Promover> BuscarTodos();
    }
    #endregion

    public class ServPromover : IServPromover
    {
        #region ctor
        private DataContext _dataContext;
        private IMovimentoFinanceiroHelper _movimentoFinanceiroHelper;
        private IPizzaHelper _pizzaHelper;

        public ServPromover(DataContext dataContext, IMovimentoFinanceiroHelper movimentoFinanceiroHelper, IPizzaHelper pizzaHelper)
        {
            _dataContext = dataContext;
            _movimentoFinanceiroHelper = movimentoFinanceiroHelper;
            _pizzaHelper = pizzaHelper;
        }
        #endregion

        #region Crud
        public void Inserir(InserirPromoverDTO inserirPromoverDto)
        {
            var promover = new Promover()
            {
                CodigoPizza = inserirPromoverDto.CodigoPizza,
                DataVigencia = inserirPromoverDto.DataVigencia,
                Descricao = inserirPromoverDto.Descricao,
                Status = EnumStatusPromover.EmAberto,
                Valor = inserirPromoverDto.Valor
            };

            _dataContext.Add(promover);
            _dataContext.SaveChanges();
        }

        public void Editar(int id, EditarPromoverDTO editarPromoverDto)
        {
            var promover = _dataContext.Promover.FirstOrDefault(p => p.Id == id);

            promover.Descricao = editarPromoverDto.Descricao;
            promover.DataVigencia = editarPromoverDto.DataVigencia;
            promover.Valor = editarPromoverDto.Valor;

            _dataContext.SaveChanges();
        }

        public List<Promover> BuscarTodos()
        {
            var listaPromover = _dataContext.Promover.ToList();

            return listaPromover;
        }
        #endregion

        #region Efetivar
        public void Efetivar(int codigoPromover)
        {
            var promover = _dataContext.Promover.FirstOrDefault(p => p.Id == codigoPromover);

            var transacao = CarregarTransacao(promover);

            try
            {
                if (!transacao.ProcessamentoInterno)
                {
                    ProcessamentoInternoEfetivar(promover);

                    transacao.ProcessamentoInterno = true;
                    _dataContext.SaveChanges();
                }

                if (!transacao.InserirMovimento)
                {
                    InserirMovimentoFinanceiroDeEntrada(promover);

                    transacao.InserirMovimento = true;
                    _dataContext.SaveChanges();
                }

                if (!transacao.AtualizarPizza)
                {
                    AtualizarPizza(promover);

                    transacao.AtualizarPizza = true;
                    _dataContext.SaveChanges();
                }

                transacao.SituacaoTransacao = EnumSituacaoTransacao.Concluido;
                promover.Status = EnumStatusPromover.Efetivado;

                _dataContext.SaveChanges();
            }
            catch (Exception e)
            {
                CompensarTransacaoEfetivar(promover, transacao);

                throw e;
            }
        }

        public void ProcessamentoInternoEfetivar(Promover promover)
        {
            if (promover.Valor <= 0)
            {
                throw new Exception("Valor deve ser maior que zero.");
            }

            if (promover.DataVigencia < DateTime.Today)
            {
                throw new Exception("A vigência deve ser posterior a data de hoje");
            }
        }

        public void InserirMovimentoFinanceiroDeEntrada(Promover promover)
        {
            var movtoFinanceiro = new InserirMovtoCaixaDTO
            {
                CodigoVinculacao = promover.Id,
                TipoVinculo = 1,
                DataMovimento = promover.DataVigencia,
                Descricao = "Promocao - " + promover.Id,
                Valor = promover.Valor
            };

            _movimentoFinanceiroHelper.InserirMovimentoEntrada(movtoFinanceiro);
        }

        public void AtualizarPizza(Promover promover)
        {
            var informarPromocaoNaPizzaDto = new InformarPromocaoNaPizzaDTO()
            {
                DataVigenciaPromocao = promover.DataVigencia,
                ValorPromocao = promover.Valor
            };

            _pizzaHelper.InformarPromocaoNaPizza(promover.CodigoPizza, informarPromocaoNaPizzaDto);
        }

        public PromoverTransacaoEfetivar CarregarTransacao(Promover promover)
        {
            var transacaoExistente = _dataContext.PromoverTransacaoEfetivar.
                FirstOrDefault(p => p.CodigoPromover == promover.Id && p.SituacaoTransacao == EnumSituacaoTransacao.EmAndamento);

            if (transacaoExistente != null)
            {
                return transacaoExistente;
            }

            var novaTransacao = new PromoverTransacaoEfetivar();

            novaTransacao.CodigoPromover = promover.Id;
            novaTransacao.SituacaoTransacao = EnumSituacaoTransacao.EmAndamento;

            _dataContext.Add(novaTransacao);
            _dataContext.SaveChanges();

            return novaTransacao;
        }

        public void CompensarTransacaoEfetivar(Promover promover, PromoverTransacaoEfetivar transacao)
        {
            if (transacao.ProcessamentoInterno)
            {
                transacao.ProcessamentoInterno = false;
                _dataContext.SaveChanges();
            }

            if (transacao.InserirMovimento)
            {
                InserirMovimentoFinanceiroDeSaida(promover);

                transacao.InserirMovimento = false;
                _dataContext.SaveChanges();
            }

            if (transacao.AtualizarPizza)
            {
                DesfazerAtualizacaoDaPizza(promover);

                transacao.AtualizarPizza = false;
                _dataContext.SaveChanges();
            }

            _dataContext.Remove(transacao);
            promover.Status = EnumStatusPromover.EmAberto;
            _dataContext.SaveChanges();
        }

        public void InserirMovimentoFinanceiroDeSaida(Promover promover)
        {
            var movtoFinanceiroDto = new InserirMovtoCaixaDTO
            {
                CodigoVinculacao = promover.Id,
                TipoVinculo = 1,
                DataMovimento = promover.DataVigencia,
                Descricao = "Estorno de promocao - " + promover.Id,
                Valor = promover.Valor
            };

            _movimentoFinanceiroHelper.InserirMovimentoSaida(movtoFinanceiroDto);
        }


        public void DesfazerAtualizacaoDaPizza(Promover promover)
        {
            var informarPromocaoNaPizzaDto = new InformarPromocaoNaPizzaDTO()
            {
                DataVigenciaPromocao = DateTime.MinValue,
                ValorPromocao = 0
            };

            _pizzaHelper.InformarPromocaoNaPizza(promover.CodigoPizza, informarPromocaoNaPizzaDto);
        }
        #endregion

        #region EfetivarAssincrono
        public void EfetivarAssincrono(int codigoPromover)
        {
            var promover = _dataContext.Promover.FirstOrDefault(p => p.Id == codigoPromover);

            if (promover.Status != EnumStatusPromover.EmAberto)
            {
                throw new Exception("Promoção já efetivada.");
            }

            var transacao = CarregarTransacaoAssincrona(promover);

            try
            {
                if (!transacao.ProcessamentoInterno)
                {
                    ProcessamentoInternoEfetivar(promover);

                    transacao.ProcessamentoInterno = true;
                    _dataContext.SaveChanges();
                }

                if (!transacao.InserirMovimento)
                {
                    InserirMovimentoFinanceiroDeEntradaAssincrono(promover);

                    transacao.InserirMovimento = true;
                    _dataContext.SaveChanges();
                }

                if (!transacao.AtualizarPizza)
                {
                    AtualizarPizzaAssincrono(promover);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PromoverTransacaoEfetivarAssincrona CarregarTransacaoAssincrona(Promover promover)
        {
            var transacaoExistente = _dataContext.PromoverTransacaoEfetivarAssincrona.
                FirstOrDefault(p => p.CodigoPromover == promover.Id && p.SituacaoTransacao == EnumSituacaoTransacao.EmAndamento);

            if (transacaoExistente != null)
            {
                return transacaoExistente;
            }

            var novaTransacao = new PromoverTransacaoEfetivarAssincrona();

            novaTransacao.CodigoPromover = promover.Id;
            novaTransacao.SituacaoTransacao = EnumSituacaoTransacao.EmAndamento;
            novaTransacao.MensagemDeErroAtualizarPizza = " ";

            _dataContext.Add(novaTransacao);
            _dataContext.SaveChanges();

            return novaTransacao;
        }

        public void InserirMovimentoFinanceiroDeEntradaAssincrono(Promover promover)
        {
            var movtoFinanceiro = new InserirMovtoCaixaDTO
            {
                CodigoVinculacao = promover.Id,
                TipoVinculo = 1,
                DataMovimento = promover.DataVigencia,
                Descricao = "Promocao - " + promover.Id,
                Valor = promover.Valor
            };

            PublishInserirMovtoDeEntrada.PublicarInserirMovimentoDeEntrada(movtoFinanceiro);
        }

        public void AtualizarPizzaAssincrono(Promover promover)
        {
            var informarPromocaoNaPizzaDto = new InformarPromocaoNaPizzaPorFilaDTO()
            {
                CodigoPizza = promover.CodigoPizza,
                DataVigenciaPromocao = promover.DataVigencia,
                ValorPromocao = promover.Valor
            };

            PublishInformarPromocaoNaPizza.PublicarInformarPromocaoNaPizza(informarPromocaoNaPizzaDto);
        }

        public void ReceberResultInformarResultadoPromocaoAssincrono(ResultInformarPromoverPizza resultInformarPromoverPizza)
        {
            var promover = _dataContext.Promover.Where(p => p.CodigoPizza == resultInformarPromoverPizza.CodigoPizza && p.Status == EnumStatusPromover.EmAberto).FirstOrDefault();

            var transacao = _dataContext.PromoverTransacaoEfetivarAssincrona.FirstOrDefault(p => p.CodigoPromover == promover.Id);

            promover.Status = EnumStatusPromover.Efetivado;
            transacao.AtualizarPizza = true;
            transacao.SituacaoTransacao = EnumSituacaoTransacao.Concluido;

            _dataContext.SaveChanges();
        }

        public string VerificarAndamentoProcessamento(int codigoPromover)
        {
            var transacao = _dataContext.PromoverTransacaoEfetivarAssincrona.FirstOrDefault(p => p.CodigoPromover == codigoPromover);

            string retorno;

            if (transacao.SituacaoTransacao == EnumSituacaoTransacao.Concluido)
            {
                retorno = "Transação finalizada";
            }
            else
            {
                retorno = "Transação em aberto";
            }

            if (!transacao.ProcessamentoInterno)
            {
                retorno = retorno + "\nProcessamento interno pendente";
            }

            if (!transacao.InserirMovimento)
            {
                retorno = retorno + "\nMovimento do caixa pendente";
            }

            if (!transacao.AtualizarPizza)
            {
                retorno = retorno + "\nAtualizacao da pizza pendente";
            }

            return retorno;
        }
        #endregion

        #region Cancelar
        public void Cancelar(int id)
        {
            var promover = _dataContext.Promover.FirstOrDefault(p => p.Id == id);

            if (promover.Status != EnumStatusPromover.EmAberto)
            {
                throw new Exception("Para cancelar, a promoção deve estar em aberto.");
            }

            promover.Status = EnumStatusPromover.Cancelado;

            _dataContext.SaveChanges();
        }
        #endregion
    }
}
