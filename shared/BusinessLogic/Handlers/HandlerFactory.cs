using BusinessLogic.Handlers.City;
using BusinessLogic.Handlers.Country;
using BusinessLogic.Handlers.HealthCheck;
using BusinessLogic.Handlers.Office;
using BusinessLogic.Handlers.Tenant;
using BusinessLogic.Handlers.Term;
using BusinessLogic.Handlers.User;
using Data.Db;
using Data.Enums;
using Helper.Configuration;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Handlers
{
    public static class HandlerFactory
    {
        public static IHandler GetHandler(ProcessType type, ILoggerFactory loggerFactory, GymOrganizerContext db, AppSettings settings)
        {
            switch (type)
            {
                case ProcessType.AddOffice:
                    return new AddOfficeHandler(db, loggerFactory, settings);
                case ProcessType.EditOffice:
                    return new EditOfficeHandler(db, loggerFactory, settings);
                case ProcessType.DeleteOffice:
                    return new DeleteOfficeHandler(db, loggerFactory, settings);

                case ProcessType.AddCity:
                    return new AddCityHandler(db, loggerFactory, settings);
                case ProcessType.EditCity:
                    return new EditCityHandler(db, loggerFactory, settings);
                case ProcessType.DeleteCity:
                    return new DeleteCityHandler(db, loggerFactory, settings);

                case ProcessType.AddCountry:
                    return new AddCountryHandler(db, loggerFactory, settings);
                case ProcessType.EditCountry:
                    return new EditCountryHandler(db, loggerFactory, settings);
                case ProcessType.DeleteCountry:
                    return new DeleteCountryHandler(db, loggerFactory, settings);

                case ProcessType.AddTerm:
                    return new AddTermHandler(db, loggerFactory, settings);
                case ProcessType.EditTerm:
                    return new EditTermHandler(db, loggerFactory, settings);
                case ProcessType.DeleteTerm:
                    return new DeleteTermHandler(db, loggerFactory, settings);

                case ProcessType.AddTenant:
                    return new AddTenantHandler(db, loggerFactory, settings);
                case ProcessType.EditTenant:
                    return new EditTenantHandler(db, loggerFactory, settings);

                case ProcessType.AddUser:
                    return new AddUserHandler(db, loggerFactory, settings);
                case ProcessType.EditUser:
                    return new EditUserHandler(db, loggerFactory, settings);
                case ProcessType.DeleteUser:
                    return new DeleteUserHandler(db, loggerFactory, settings);

                case ProcessType.HealthCheck:
                    return new HealthCheckHandler(db, loggerFactory, settings);
                default:
                    return null;
            }
        }
    }
}
