﻿namespace Wallet.Api.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string  Base = Root + "/" + Version;

        public static class Accounts
        {
            public const string GetAll = Base + "/accounts";

            public const string Create = Base + "/accounts";

            public const string GetById = Base + "/accounts/{id}";

            public const string Update = Base + "/accounts/{id}";

            public const string Delete = Base + "/accounts/{id}";

        }
    }
}
