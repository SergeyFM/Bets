﻿using System.Threading.Tasks;

namespace Bets.Application;
public interface INotificationService {
    Task<bool> SendEmailAsync(string email, string subject, string body);
}
