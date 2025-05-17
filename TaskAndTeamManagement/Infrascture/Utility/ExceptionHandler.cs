namespace TaskAndTeamManagement.Infrascture.Utility
{
    public class ExceptionHandler
    {
        public static bool IsForeignKeyConstraintViolation(Exception exception)
        {
            var innerException = exception;

            while (innerException != null)
            {
                if (innerException.Message.Contains("REFERENCE constraint", StringComparison.OrdinalIgnoreCase) ||
                    innerException.Message.Contains("FOREIGN KEY constraint", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                innerException = innerException.InnerException;
            }

            return false;
        }
    }
}
