﻿using AutoMapper;
using Data.Providers.MongoDb.Collections;
using Data.Providers.MongoDb.Interfaces;
using Domain.Entities;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly IMongoRepository<UsuarioCollection> _usuarioRepository;
        private readonly IMapper _mapper;
        public UsuarioRepository(
            IMongoRepository<UsuarioCollection> usuarioRepository,
            IMapper mapper
        )
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<Usuario> Autenticar(string login, string senha)
        {
            var usuarioCollection = await _usuarioRepository.FindOneAsync(filtro =>
                        filtro.Login == login && filtro.Senha == senha);
            return _mapper.Map<Usuario>(usuarioCollection);


        }

        public async Task Cadastrar(Usuario novoUsuario)
        {
            var novoUsuarioCollection = _mapper.Map<UsuarioCollection>(novoUsuario);
            await _usuarioRepository.InsertOneAsync(novoUsuarioCollection);
        }
    }
}
