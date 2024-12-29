namespace BiddingApp.BuildingBlock.Utilities
{
    public class SystemConstants
    {
        public class ModelStateResponses
        {
            public const string ModelStateInvalid = "Invalid data provided";
        }
        public class AuthenticateResponses
        {
            public const string IncorrectPassword = "Incorrect password.";
            public const string UserAuthenticated = "Authenticated successfully.";
            public const string UserRegistered = "Register successfully.";
            public const string EmailChecked = "Email is already registered. Please use a different email.";
            public const string UsernameChecked = "Username is already registered. Please use a different username.";
            public const string UserChecked = "User not exist!";
        }

        public class BiddingSessionMessageResponses
        {
            public const string BiddingSessionCreated = "New session created successfully";
            public const string BiddingSessionUpdated = "Session updated successfully";
        }

        public class BiddingMessageResponses
        {
            public const string BiddingCreated = "Bidding Successfully";
            public const string BiddingUpdated = "Session updated successfully";
        }

        public class InternalMessageResponses
        {
            public const string InternalMessageError = "Error from server.";
            public const string DatabaseBadResponse = "Error when called store procedure. ";
        }
    }
}
