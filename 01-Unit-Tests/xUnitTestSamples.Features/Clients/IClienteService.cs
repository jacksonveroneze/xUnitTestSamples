﻿using System;
using System.Collections.Generic;

namespace xUnitTestSamples.Features.Clients
{
    public interface IClienteService : IDisposable
    {
        IEnumerable<Cliente> ObterTodosAtivos();

        void Adicionar(Cliente cliente);

        void Atualizar(Cliente cliente);

        void Remover(Cliente cliente);

        void Inativar(Cliente cliente);
    }
}