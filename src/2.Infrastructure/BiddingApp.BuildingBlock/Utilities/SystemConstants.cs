namespace BiddingApp.BuildingBlock.Utilities
{
    public class SystemConstants
    {
        public class CommonResponse
        {
            public const string FetchSuccess= "Fetch data successfully";
            public const string FetchFailed = "Fetch data failed";
            public const string UpdateSuccess = "Update successfully";
            public const string NoData = "No data";
            public const string CreateSuccess = "Create Successfully";
            public const string CreateFailed = "Create Failed";
            public const string DeleteSuccess = "Detele successfully";
        }
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
            public const string UserNotExist = "User not exist!";
            public const string UserBudgetCheck = "Budget not available to bid";
        }

        public class VehicleMessageResponses
        {
            public const string VINExisted = "Vehicle VIN already existed";
            public const string VehicleNotFound = "Vehicle not found";
            public const string VehicleGuard = "System just allow for delete the vehicle with status is 'not available' only";
        }
        public class BiddingSessionMessageResponses
        {
            public const string BiddingSessionClosed = "Bidding session was closed";
            public const string BiddingSessionNotFound = "Session not found";
            public const string BiddingSessionCreateFailed = "Can not create new bidding session, vehicle is not available";
        }

        public class BiddingMessageResponses
        {
            public const string BiddingCreated = "Bidding Successfully";
            public const string BiddingNotValid = "Bidding value must be greater than current value";

        }

        public class InternalMessageResponses
        {
            public const string InternalMessageError = "Error from server.";
            public const string DatabaseBadResponse = "Error when called store procedure.";
        }
    }
}
