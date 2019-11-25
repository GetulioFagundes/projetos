using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Teste.Models;

namespace Teste.Controllers
{
    public class UserController : ControllerBase
    {
        private IUsuarioService _userService;
        private IMapper _mapper;
        private readonly AppSetting _appSetting;

    }
}