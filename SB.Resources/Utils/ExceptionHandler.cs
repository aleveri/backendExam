using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SB.Interfaces;
using System;
using System.Data.SqlClient;
using static SB.Entities.Enums;

namespace SB.Resources
{
    public class ExceptionHandler: IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger) => _logger = logger;
        

        public string GetMessage(Exception ex)
        {
            if (ex is DbUpdateException dbUpdateEx)
                if (dbUpdateEx.InnerException?.InnerException != null)
                    if (dbUpdateEx.InnerException.InnerException is SqlException sqlException)
                    {
                        _logger.LogError(
                            LoggingEvents.DbUpdateException.ToString(),
                            sqlException,
                            $"[InnerException: {ex.InnerException}][StackTrace: {ex.StackTrace}]");

                        return Messages.DbError.ToString();
                    }

            if (ex is ApplicationException applicationEx)
            {
                _logger.LogInformation(LoggingEvents.OwnException.ToString(), ex, ex.Message.ToString());
                return ex.Message;
            }

            _logger.LogCritical(LoggingEvents.Fatal.ToString(), ex, $"[InnerException: {ex.InnerException}][StackTrace: {ex.StackTrace}]");

            return Messages.AdminError.ToString();
        }
    }
}
