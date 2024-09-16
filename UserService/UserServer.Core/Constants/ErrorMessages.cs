using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.Core.Constants
{
    public class ErrorMessages
    {
        public const string UserAlreadyExists = "Пользователь с таким email уже существует.";
        public const string InvalidCredentials = "Неверные учетные данные.";
        public const string TokenExpired = "Токен истек.";
    }
}
