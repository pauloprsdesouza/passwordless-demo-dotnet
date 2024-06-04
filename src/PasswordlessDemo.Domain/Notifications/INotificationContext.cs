using System;
using System.Collections.Generic;

namespace PasswordlessDemo.Domain.Notifications
{
    public interface INotificationContext
    {
        List<string> GetValidationErrors();
        List<string> GetNotFoundErrors();
        public List<string> GetForbiddenErrors();
        bool AreThereForbiddenErros();
        bool AreThereValidationErrors();
        bool AreThereNotFoundErros();
        void AddValidationError(string message);
        void AddValidationError<TEnum>(TEnum message) where TEnum : Enum;
        void AddNotFoundError<TEnum>(TEnum message) where TEnum : Enum;
        void AddForbiddenError<TEnum>(TEnum message) where TEnum : Enum;
    }
}
